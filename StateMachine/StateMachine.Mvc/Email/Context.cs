namespace StateMachine.MVC.Email;

public class Context
{
    private readonly List<Match> _matches = new();

    public Context(string? textToScan)
    {
        TextToScan = textToScan ?? "";
    }

    private string TextToScan { get; }

    public IEnumerable<Match> Matches => _matches.AsReadOnly();

    public bool IsComplete => string.IsNullOrWhiteSpace(TextToScan) || CurrentPosition >= TextToScan.Length;

    public int CurrentPosition { get; set; }

    /// <summary>
    /// Returns current character or char(0) if at end of string
    /// </summary>
    public char CurrentCharacter => CurrentPosition >= TextToScan.Length ? (char)0 : TextToScan[CurrentPosition];
    
    private int CurrentStartIndex { get; set; }

    public void AddMatch()
    {
        var emailAddress = TextToScan.Substring(CurrentStartIndex, CurrentPosition - CurrentStartIndex);
        _matches.Add(new Match(emailAddress, CurrentStartIndex));
    }

    public void AdvancePosition()
    {
        CurrentPosition++;
    }

    public void SetCurrentStartIndex()
    {
        CurrentStartIndex = CurrentPosition;
    }
}