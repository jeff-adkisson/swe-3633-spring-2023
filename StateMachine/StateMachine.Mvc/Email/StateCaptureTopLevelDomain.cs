namespace StateMachine.MVC.Email;

public class StateCaptureTopLevelDomain : IState
{

    public IState GetNextState(Context context)
    {
        var length = 0;
        while (ContinueLooping(context))
        {
            context.AdvancePosition();
            length++;
        }

        var isValidLength = length is >= 2 and <= 10;
        return !isValidLength
            ? StateFactory.Get<StateAdvanceToNextWord>()
            : StateFactory.Get<StateAddEmailAddress>();
    }

    private static bool ContinueLooping(Context context)
    {
        return CharacterMatch.IsAlphanumeric(context.CurrentCharacter);
    }
}