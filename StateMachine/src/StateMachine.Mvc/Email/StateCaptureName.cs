namespace StateMachine.MVC.Email;

public class StateCaptureName : IState
{
    public IState Process(Context context)
    {
        context.SetCurrentStartIndex();
        
        var length = 0;
        while (ContinueLooping(context))
        {
            context.AdvancePosition();
            length++;
        }

        if (length <= 0 || !CharacterMatch.IsAmpersand(context.CurrentCharacter))
            return this.GetNextState<StateAdvanceToNextWord>();
        
        context.AdvancePosition();
        return this.GetNextState<StateCaptureDomain>();

    }

    private static bool ContinueLooping(Context context)
    {
        return CharacterMatch.IsAlphanumericOrSymbol(context.CurrentCharacter);
    }
}