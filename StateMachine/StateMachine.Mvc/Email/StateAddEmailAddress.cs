namespace StateMachine.MVC.Email;

public class StateAddEmailAddress : IState
{
    public IState GetNextState(Context context)
    {
        context.AddMatch();
        context.AdvancePosition();

        return StateFactory.Get<StateAdvanceToNextWord>();
    }
}