namespace StateMachine.MVC.Email;

public static class Finder
{
    public static Match[] Find(string? text)
    {
        var context = new Context(text);
        var state = StateFactory.Get<StateStartOfWord>();
        while (!context.IsComplete) {
            state = state.GetNextState(context);
        }

        return context.Matches.ToArray();
    }
}