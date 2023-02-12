namespace StateMachine.MVC.Email;

public class StateAdvanceToNextWord : StateBase
{
    public StateAdvanceToNextWord(Context context) : base(context)
    {
    }

    public override IState GetNextState()
    {
        while (ContinueLooping())
        {
            Context.AdvancePosition();
        }
        return new StateStartOfWord(Context);
    }

    private bool ContinueLooping()
    {
        return !Context.IsComplete && !CharacterMatch.IsWordBreak(Context.CurrentCharacter);
    }
}