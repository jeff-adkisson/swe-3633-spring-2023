namespace StateMachine.MVC.Email;

public static class Finder
{
    /// <summary>
    /// Accepts a string and returns an array of valid email addresses.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="normalizeLineEndings">If true, sets line endings to /n to work consistently cross platform</param>
    /// <returns></returns>
    public static Match[] Find(string? text, bool normalizeLineEndings = true)
    {
        var context = new Context(text, normalizeLineEndings);
        
        var currentState = NextState.Start();
        
        while (!currentState.IsComplete()) {
            currentState = currentState.Process(context);
        }

        return context.Matches.ToArray();
    }
}