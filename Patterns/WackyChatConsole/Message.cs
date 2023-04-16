using System.Text.RegularExpressions;

namespace WackyChatConsole;

public class Message
{
    private static readonly Regex StripInvalidRegex = new(@"[^a-zA-Z0-9 \.!]");
    private string _text = "TextNotSet";

    public DateTime SentAt { get; set; } = DateTime.Now;

    public string From { get; set; } = "FromNotSet";

    public string Text
    {
        get => StripInvalidRegex.Replace(_text, "");
        set => _text = value;
    }

    public override string ToString()
    {
        return $"{From} at {SentAt} | {Text}";
    }
}