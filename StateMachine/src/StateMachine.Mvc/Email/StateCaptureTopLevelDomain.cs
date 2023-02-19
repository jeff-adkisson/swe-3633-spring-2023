namespace StateMachine.MVC.Email;

public class StateCaptureTopLevelDomain : IState
{

    public IState Process(Context context)
    {
        var length = 0;
        while (ContinueLooping(context))
        {
            context.AdvancePosition();
            length++;
        }

        var isValidLength = length is >= 2 and <= 10;
        return !isValidLength
            ? this.GetNextState<StateAdvanceToNextWord>()
            : this.GetNextState<StateAddEmailAddress>();
    }

    private static bool ContinueLooping(Context context)
    {
        return CharacterMatch.IsAlphanumeric(context.CurrentCharacter);
    }
}