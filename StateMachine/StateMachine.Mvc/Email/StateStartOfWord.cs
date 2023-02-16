using System.Diagnostics;

namespace StateMachine.MVC.Email;

public class StateStartOfWord : IState
{
    public IState GetNextState(Context context)
    {
        while (!context.IsComplete)
        {
            if (CharacterMatch.IsAlphanumericOrSymbol(context.CurrentCharacter))
                return StateFactory.Get<StateCaptureName>();
            
            context.AdvancePosition();
        }

        return this;
    }
}