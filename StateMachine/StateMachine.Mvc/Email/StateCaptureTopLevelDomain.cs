namespace StateMachine.MVC.Email;

public class StateCaptureTopLevelDomain : StateBase
{
    public StateCaptureTopLevelDomain(Context context) : base(context)
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

        var isValidLength = length is >= 2 and <= 10;
        if (!isValidLength)
            return new StateAdvanceToNextWord(Context);

        return new StateAddEmailAddress(Context);
    }

    private bool ContinueLooping()
    {
        return CharacterMatch.IsAlphanumeric(Context.CurrentCharacter);
    }
}