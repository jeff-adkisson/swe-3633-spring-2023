namespace StateMachine.MVC.Email;

public class StateCaptureDomain : IState
{
    public IState GetNextState(Context context)
    {
        var length = 0;

        while (ContinueLooping(context))
        {
            context.AdvancePosition();
            length++;
        }

        if (length == 0 || !CharacterMatch.IsDot(context.CurrentCharacter)) 
            return new StateAdvanceToNextWord();
        
        context.AdvancePosition();
        return StateFactory.Get<StateCaptureTopLevelDomain>();
    }

    private static bool ContinueLooping(Context context)
    {
        return CharacterMatch.IsAlphanumeric(context.CurrentCharacter);
    }
}