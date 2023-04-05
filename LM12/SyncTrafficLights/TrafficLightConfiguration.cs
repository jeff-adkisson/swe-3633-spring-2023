namespace SyncTrafficLights;

public record TrafficLightConfiguration(
    TrafficLight TrafficLight, 
    State InitialState, 
    Dictionary<State, StateTransition> StateTransitions);