namespace StateMachine.MVC.Email;

public class StateCaptureName : IState
{
    public IState GetNextState(Context context)
    {
        context.SetCurrentStartIndex();
        
        var length = 0;
        while (ContinueLooping(context))
        {
            context.AdvancePosition();
            length++;
        }

        if (length <= 0 || !CharacterMatch.IsAmpersand(context.CurrentCharacter))
            return StateFactory.Get<StateAdvanceToNextWord>();
        
        context.AdvancePosition();
        return StateFactory.Get<StateCaptureDomain>();

    }

    private static bool ContinueLooping(Context context)
    {
        return CharacterMatch.IsAlphanumericOrSymbol(context.CurrentCharacter);
    }
}