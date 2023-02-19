namespace StateMachine.MVC.Email;

public class StateAdvanceToNextWord : IState
{
    public IState Process(Context context)
    {
        while (ContinueLooping(context))
        {
            context.AdvancePosition();
        }
        return this.GetNextState<StateStartOfWord>();
    }

    private static bool ContinueLooping(Context context)
    {
        return !context.IsComplete && !CharacterMatch.IsWordBreak(context.CurrentCharacter);
    }
}