namespace StateMachine.MVC.Email;

public class StateCaptureDomain : StateBase
{
    public StateCaptureDomain(Context context) : base(context)
    {
    }

    public override IState GetNextState()
    {
        var length = 0;

        while (ContinueLooping())
        {
            Context.AdvancePosition();
            length++;
        }

        if (length == 0 || !CharacterMatch.IsDot(Context.CurrentCharacter)) 
            return new StateAdvanceToNextWord(Context);
        
        Context.AdvancePosition();
        return new StateCaptureTopLevelDomain(Context);

    }

    private bool ContinueLooping()
    {
        return CharacterMatch.IsAlphanumeric(Context.CurrentCharacter);
    }
}