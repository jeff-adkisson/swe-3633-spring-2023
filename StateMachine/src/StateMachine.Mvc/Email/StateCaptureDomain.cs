namespace StateMachine.MVC.Email;

public class StateCaptureDomain : IState
{
    public IState Process(Context context)
    {
        var length = 0;

        while (ContinueLooping(context))
        {
            context.AdvancePosition();
            length++;
        }

        if (length == 0 || !CharacterMatch.IsDot(context.CurrentCharacter)) 
            return this.GetNextState<StateAdvanceToNextWord>();
        
        context.AdvancePosition();
        return this.GetNextState<StateCaptureTopLevelDomain>();
    }

    private static bool ContinueLooping(Context context)
    {
        return CharacterMatch.IsAlphanumeric(context.CurrentCharacter);
    }
}