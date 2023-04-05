namespace SyncTrafficLights;

public record StateTransition(State CurrentState, int ThenWaitSeconds, State ThenTransitionTo, bool IsInitialState = false);