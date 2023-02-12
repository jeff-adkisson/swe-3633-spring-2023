namespace StateMachine.MVC.Email;

public static class Finder
{
    public static Match[] Find(string? text)
    {
        var context = new Context(text);
        var state = new StateStartOfWord(context) as IState;
        while (!context.IsComplete) {
            state = state.GetNextState();
        }

        return context.Matches.ToArray();
    }
}