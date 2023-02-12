namespace StateMachine.MVC.Email;

public class StateAddEmailAddress : StateBase
{
    public StateAddEmailAddress(Context context) : base(context)
    {
    }

    public override IState GetNextState()
    {
        Context.AddMatch();
        Context.AdvancePosition();

        return new StateAdvanceToNextWord(Context);
    }
}