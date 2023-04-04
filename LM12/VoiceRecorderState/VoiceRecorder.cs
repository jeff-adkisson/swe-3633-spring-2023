namespace VoiceRecorderState;

public class VoiceRecorder
{
    private State CurrentState { get; set; } = State.Off; //starting state

    private State[] PermittedTransitions => TransitionTable.Rules[CurrentState];

    //start simulation here
    public static void Start()
    {
        var instance = new VoiceRecorder();
        instance.Execute();
    }

    private void Execute() {
        while (CurrentState != State.TerminateSimulation)
        {
            ShowCurrentState();
            var priorState = CurrentState;
            CurrentState = GetNextState();
            ShowTransition(priorState);
        }
    }

    private void ShowTransition(State priorState)
    {
        Console.WriteLine($"> Transitioning from [{priorState}] to [{CurrentState}]");
    }

    private void ShowCurrentState()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"> CURRENT STATE: {CurrentState.ToString().ToUpper()}");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine();
    }

    private State GetNextState()
    {
        //if only one state, advance automatically
        if (PermittedTransitions.Length == 1) return PermittedTransitions[0]; 
        
        //loop until we get valid input
        while (true)
        {
            ShowAvailableTransitions();

            var input = GetKeyboardInput();
            if (string.IsNullOrWhiteSpace(input)) return State.TerminateSimulation;

            //parse input and return next state if valid input AND permitted transition
            if (Enum.TryParse<State>(input, true, out var nextState))
            {
                if (PermittedTransitions.Contains(nextState)) return nextState;
            }
            
            HandleInvalidInput(input);
        }
    }

    private void HandleInvalidInput(string input)
    {
        Console.WriteLine();
        Console.WriteLine(new string('!', 50));
        Console.WriteLine(
            $"INVALID TRANSITION REQUESTED [{input}]. TRY AGAIN.");
        Console.WriteLine(new string('!', 50));

        ShowCurrentState();
    }

    private static string? GetKeyboardInput()
    {
        Console.Write("> ");
        var input = Console.ReadLine();

        return input;
    }

    private void ShowAvailableTransitions()
    {
        Console.WriteLine("Next State [empty input ends simulation]:");
        foreach (var transition in PermittedTransitions)
        {
            Console.WriteLine($" => {transition.ToString().ToLower()}");
        }
    }
}