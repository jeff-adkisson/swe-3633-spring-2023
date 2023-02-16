namespace StateMachine.MVC.Email;

public class StateAdvanceToNextWord : IState
{
    public IState GetNextState(Context context)
    {
        while (ContinueLooping(context))
        {
            context.AdvancePosition();
        }
        return StateFactory.Get<StateStartOfWord>();
    }

    private static bool ContinueLooping(Context context)
    {
        return !context.IsComplete && !CharacterMatch.IsWordBreak(context.CurrentCharacter);
    }
}