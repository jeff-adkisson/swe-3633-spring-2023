namespace StateMachine.MVC.Email;

public static class CharacterMatch
{
    /// <summary>
    /// Alphanumeric characters permitted in some parts of an email address
    /// </summary>
    private const string Alphanumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    /// <summary>
    ///  Alphanumeric characters and symbols permitted in some parts of an email address
    /// </summary>
    private const string AlphanumericAndSymbols = Alphanumeric + ".-";
    
    public static bool IsAlphanumeric(char character) => Alphanumeric.Contains(character);
    
    public static bool IsAlphanumericOrSymbol(char character) => AlphanumericAndSymbols.Contains(character);
    
    public static bool IsWordBreak(char character) => character == ' ' || character == '\t' || character == '\r' || character == '\n';
    
    public static bool IsAmpersand(char character) => character == '@';
    
    public static bool IsDot(char character) => character == '.';
}