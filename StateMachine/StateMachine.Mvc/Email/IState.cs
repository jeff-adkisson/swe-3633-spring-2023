namespace StateMachine.MVC.Email;

public interface IState
{
    IState GetNextState();
}