namespace StateMachine.MVC.Email;

public interface IState
{
    /// <summary>
    /// Processes the context and returns the next state.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    IState Process(Context context);
}