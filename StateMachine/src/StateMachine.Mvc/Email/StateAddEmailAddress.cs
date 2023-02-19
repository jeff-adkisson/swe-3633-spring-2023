namespace StateMachine.MVC.Email;

public class StateAddEmailAddress : IState
{
    public IState Process(Context context)
    {
        context.AddMatch();
        context.AdvancePosition();

        return this.GetNextState<StateAdvanceToNextWord>();
    }
}