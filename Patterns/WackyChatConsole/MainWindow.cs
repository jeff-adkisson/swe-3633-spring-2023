using Terminal.Gui;

namespace WackyChatConsole;

//UI borrowed (with updates) from https://github.com/0x414c49/gui-cs-chat-sample
//see https://itnext.io/terminal-console-user-interface-in-net-core-4e978f1225b for original article

public class MainWindow
{
    public delegate void SendMessageHandler(string message);

    private readonly List<string> _messages = new();
    private ListView? _chatView;
    private bool _isStarted;
    private string _username = string.Empty;

    public void Start()
    {
        var gui = DrawMainWindow();
        Application.MainLoop.Invoke(() =>
        {
            _isStarted = true;
            gui.TextField.SetFocus();
        });
        Application.Run(gui.TopLevel);
    }

    public event SendMessageHandler? SendMessage;

    public void ReceiveMessage(string msg)
    {
        if (!_isStarted) return;
        Application.MainLoop.Invoke(() =>
        {
            _messages.Add(msg);
            _chatView!.SetNeedsDisplay();
        });
    }

    private Gui DrawMainWindow()
    {
        Application.Init();
        var top = Application.Top;

        var mainWindow = new Window("Wacky Chat")
        {
            X = 0,
            Y = 1, // Leave one row for the toplevel menu

            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        top.Add(
            new MenuBar(new MenuBarItem[]
            {
                new("_Quit", "", () => Application.RequestStop())
            })
        );

        var chatViewFrame = new FrameView("Messages")
        {
            X = 0,
            Y = 1,
            Width = Dim.Percent(100),
            Height = mainWindow.Height - 5,
            Border = new Border { BorderStyle = BorderStyle.None }
        };

        _chatView = new ListView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        _chatView.SetSource(_messages);
        chatViewFrame.Add(_chatView);
        mainWindow.Add(chatViewFrame);

        var chatBar = new FrameView(null)
        {
            X = 0,
            Y = Pos.Bottom(chatViewFrame),
            Width = chatViewFrame.Width,
            Height = Dim.Fill()
        };

        var chatMessage = new TextField("")
        {
            X = 3,
            Y = 1,
            Width = Dim.Percent(80),
            Height = Dim.Sized(1)
        };

        var sendButton = new Button("Send", true)
        {
            X = Pos.Right(chatMessage) + 3,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Sized(1)
        };

        sendButton.Clicked += () =>
        {
            Application.MainLoop.Invoke(() =>
            {
                var msg = chatMessage.Text.ToString();
                if (string.IsNullOrWhiteSpace(msg)) return;

                SendMessage?.Invoke(msg);

                chatMessage.Text = "";
                chatMessage.SetFocus();
            });
        };

        chatBar.Add(chatMessage);
        chatBar.Add(sendButton);
        mainWindow.Add(chatBar);

        top.Add(mainWindow);

        return new Gui(top, chatMessage);
    }

    private record Gui(Toplevel TopLevel, TextField TextField);
}