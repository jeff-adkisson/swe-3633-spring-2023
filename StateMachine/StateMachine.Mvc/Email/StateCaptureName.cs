namespace StateMachine.MVC.Email;

public class StateCaptureName : StateBase
{
    public StateCaptureName(Context context) : base(context)
    {
    }

    public override IState GetNextState()
    {
        Context.SetCurrentStartIndex();
        
        var length = 0;
        while (ContinueLooping())
        {
            Context.AdvancePosition();
            length++;
        }

        if (length > 0 && CharacterMatch.IsAmpersand(Context.CurrentCharacter))
        {
            Context.AdvancePosition();
            return new StateCaptureDomain(Context);
        }

        return new StateAdvanceToNextWord(Context);

    }

    private bool ContinueLooping()
    {
        return CharacterMatch.IsAlphanumericOrSymbol(Context.CurrentCharacter);
    }
}