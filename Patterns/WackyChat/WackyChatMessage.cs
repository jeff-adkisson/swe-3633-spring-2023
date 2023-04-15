using System.Text.RegularExpressions;

namespace WackyChat;

public class WackyChatMessage
{
    private string _text = "TextNotSet";
    private static readonly Regex StripInvalidRegex = new Regex(@"[^a-zA-Z0-9 \.!]");

    public DateTime SentAt { get; set; } = DateTime.Now;
    public string From { get; set; } = "FromNotSet";

    public string Text
    {
        get => StripInvalidRegex.Replace(_text, "");
        set => _text = value;
    }

    public override string ToString() => $"{From} at {SentAt} | {Text}";
}