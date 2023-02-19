namespace StateMachine.MVC.Email;

public class Context
{
    private readonly List<Match> _matches = new();

    /// <summary>
    /// Context of email address finder state machine.
    /// </summary>
    /// <param name="textToScan">Multiline text block to scan for valid addresses</param>
    /// <param name="normalizeLineEndings">If true, normalizes line endings across platforms</param>
    public Context(string? textToScan, bool normalizeLineEndings = true)
    {
        TextToScan = normalizeLineEndings
            ? (textToScan ?? "").Replace("\r\n", "\n")
            : (textToScan ?? "");
    }

    private string TextToScan { get; }

    public IEnumerable<Match> Matches => _matches.AsReadOnly();

    public bool IsComplete => string.IsNullOrWhiteSpace(TextToScan) || CurrentPosition >= TextToScan.Length;

    /// <summary>
    /// The current position of the state machine in the text block
    /// </summary>
    private int CurrentPosition { get; set; }

    /// <summary>
    /// Returns current character or char(0) if at end of string
    /// </summary>
    public char CurrentCharacter => CurrentPosition >= TextToScan.Length ? (char)0 : TextToScan[CurrentPosition];
    
    /// <summary>
    /// Pointer to the current word (which is possibly an email address)
    /// </summary>
    private int CurrentStartIndex { get; set; }

    /// <summary>
    /// Add an string from the current start index to the current position to the matches list
    /// </summary>
    public void AddMatch()
    {
        var emailAddress = TextToScan.Substring(CurrentStartIndex, CurrentPosition - CurrentStartIndex);
        _matches.Add(new Match(emailAddress, CurrentStartIndex));
    }

    /// <summary>
    /// Advance the character pointer by one
    /// </summary>
    public void AdvancePosition()
    {
        CurrentPosition++;
    }

    /// <summary>
    /// Call when the start of a new word (which is possibly an email address) is found
    /// </summary>
    public void SetCurrentStartIndex()
    {
        CurrentStartIndex = CurrentPosition;
    }
}