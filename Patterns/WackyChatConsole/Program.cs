namespace WackyChatConsole;
// Note: actual namespace depends on the project name.

internal class Program
{
    private static MessageQueueClient? MessageQueue { get; set; }
    private static MainWindow? MainWindow { get; set; }

    internal static async Task Main()
    {
        await Connect();
        MainWindow = new MainWindow();

        //configure events connecting UI and queue
        MainWindow.SendMessage += OnSend;
        MessageQueue!.Receive += OnReceive;

        MainWindow.Start();
    }

    private static void OnReceive(Message msg)
    {
        MainWindow?.ReceiveMessage(msg.ToString());
    }

    private static void OnSend(string msg)
    {
        MessageQueue?.Send(msg);
    }

    private static async Task Connect()
    {
        MessageQueue = new MessageQueueClient();
        var successConnect = await MessageQueue.Connect();
        if (!successConnect)
        {
            ShowConnectFailure();
            return;
        }

        //configure event to help ensure we disconnect cleanly (nothing is guaranteed)
        AppDomain.CurrentDomain.ProcessExit += Disconnect;
    }

    private static void ShowConnectFailure()
    {
        Console.Error.WriteLine("Could not connect to message bus");
        Console.Error.WriteLine("Check appsettings.json config file.");
    }

    private static void Disconnect(object? sender, EventArgs e)
    {
        MainWindow!.SendMessage -= OnSend;
        MessageQueue!.Receive -= OnReceive;
        MessageQueue?.Disconnect();
    }
}