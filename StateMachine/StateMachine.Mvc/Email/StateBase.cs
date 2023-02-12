namespace StateMachine.MVC.Email;

public abstract class StateBase : IState
{
    protected StateBase(Context context)
    {
        Context = context;
    }

    protected Context Context { get; }

    public abstract IState GetNextState();
}