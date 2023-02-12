using System.Diagnostics;

namespace StateMachine.MVC.Email;

public class StateStartOfWord : StateBase
{
    public StateStartOfWord(Context context) : base(context)
    {
    }

    public override IState GetNextState()
    {
        while (!Context.IsComplete)
        {
            if (CharacterMatch.IsAlphanumericOrSymbol(Context.CurrentCharacter))
                return new StateCaptureName(Context);
            
            Context.AdvancePosition();
        }

        return this;
    }
}