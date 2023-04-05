namespace SyncTrafficLights;

public static class StateTransitions
{
    private const bool IsInitialState = true;

    public static Dictionary<TrafficLight, List<StateTransition>> GetAllTrafficLightTransitions() =>
        new()
        {
            { TrafficLight.NorthSouth, GetNorthSouthTransitions() },
            { TrafficLight.EastWest, GetEastWestStateTransitions() }
        };

    private static List<StateTransition> GetEastWestStateTransitions() => new()
    {
        new StateTransition(State.Red_Synchronize, 0, State.Green, IsInitialState),
        new StateTransition(State.Green, 40, State.Yellow),
        new StateTransition(State.Yellow, 5, State.Red),
        new StateTransition(State.Red, 0, State.Red_Synchronize)
    };
    
    private static List<StateTransition> GetNorthSouthTransitions() => new()
    {
        new StateTransition(State.Red_Synchronize, 0, State.Green),
        new StateTransition(State.Green, 30, State.Yellow),
        new StateTransition(State.Yellow, 5, State.Red),
        new StateTransition(State.Red, 0, State.Red_Synchronize, IsInitialState),
    };


}