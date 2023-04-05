using Timer = System.Timers.Timer;

namespace SyncTrafficLights;

public class TrafficLightStateMachine : IDisposable
{
    private record OtherLightState(TrafficLight TrafficLight, State CurrentState);
    
    private readonly TrafficLightConfiguration _configuration;
    
    public event EventHandler<TrafficLightSynchronizationEventArgs>? SynchronizeTrafficLights;

    public TrafficLightStateMachine(TrafficLightConfiguration configuration)
    {
        _configuration = configuration;
        SetState(_configuration.InitialState);
        
        _timer = new Timer()
        {
            AutoReset = true,
            Interval = 1 * 1000 //1 second
        };
        _timer.Elapsed += (_, _) => OnTimerElapsed();
    }

    public void Start()
    {
        Logger("Starting");
        _timer.Start();
    }

    public void Stop()
    {
        Logger("Stopping");
        _timer.Stop();
        Dispose(); //timers need to be disposed to avoid memory leaks
    }

    private void OnTimerElapsed()
    {
        if (_currentState == State.Red_Synchronize) {
            return; 
        }

        if (_secondsToHoldInCurrentState > 0)
        {
            _secondsToHoldInCurrentState--;
            ShowRemainingTime();
            return;
        }
        
        ChangeState();
    }

    private void ShowRemainingTime()
    {
        Logger($"Tick [{_secondsToHoldInCurrentState} seconds till next state]");
    }

    private State GetNextState() => _configuration.StateTransitions[_currentState].ThenTransitionTo;
    private int GetStateWaitSeconds(State state) => _configuration.StateTransitions[state].ThenWaitSeconds;

    private OtherLightState? _otherLightState;
    
    private State _currentState = State.Red_Synchronize;

    private int _secondsToHoldInCurrentState;

    private readonly Timer _timer;

    public void OnSynchronize(TrafficLightSynchronizationEventArgs args)
    {
        if (args.TrafficLight == _configuration.TrafficLight) {
            return; 
        }
        
        _otherLightState = new OtherLightState(args.TrafficLight, args.CurrentState);
        Logger($"Synchronize called from {args.TrafficLight}, changing state");
        ChangeState();
    }

    private void SetState(State state)
    {
        _currentState = state;
        _secondsToHoldInCurrentState = GetStateWaitSeconds(state);
    }

    private void ChangeState()
    {
        var currentState = _currentState;
        var newState = GetNextState();
        var newStateWaitSeconds = GetStateWaitSeconds(newState);
        Logger($"Changing state from {currentState} to {newState} then wait {newStateWaitSeconds} seconds");

        SetState(newState);
        ShowRemainingTime();
        if (_currentState != State.Red_Synchronize) return;
        
        Logger("Synchronize");
        var evtArgs = new TrafficLightSynchronizationEventArgs(_configuration.TrafficLight, currentState);
        SynchronizeTrafficLights?.Invoke(this, evtArgs);
    }
    
    static ConsoleColor GetColorForState(State state)
    {
        return state switch
        {
            State.Red or State.Red_Synchronize => ConsoleColor.Red,
            State.Yellow => ConsoleColor.Yellow,
            State.Green => ConsoleColor.Green,
            _ => ConsoleColor.White
        };
    }

    private static void ShowTrafficLightHorizontal(State state)
    {
        Console.Write("[ ");
        switch (state)
        {
            case State.Green:
                Console.Write("O O ");
                OutputInLightStateColor(state, "O");
                break;
            case State.Yellow:
                Console.Write("O ");
                OutputInLightStateColor(state, "O");
                Console.Write(" O");
                break;
            default:
                OutputInLightStateColor(state, "O");
                Console.Write(" O O");
                break;
        }
        Console.Write(" ] ");
        OutputInLightStateColor(state);
    }

    private static void OutputInLightStateColor(State state, string? output = null)
    {
        output ??= state.ToString();
        Console.ForegroundColor = GetColorForState(state);
        Console.Write(output);
        Console.ResetColor();
    }

    private void Logger(string desc)
    {
        string PadLightName(TrafficLight trafficLight) => trafficLight.ToString().PadLeft(15, ' ');

        Console.Write($"{PadLightName(_configuration.TrafficLight)}: ");
        ShowTrafficLightHorizontal(_currentState);
        Console.WriteLine($" -> {desc}");

        if (_otherLightState != null)
        {
            Console.Write($"{PadLightName(_otherLightState.TrafficLight)}: ");
            ShowTrafficLightHorizontal(_otherLightState.CurrentState);
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}