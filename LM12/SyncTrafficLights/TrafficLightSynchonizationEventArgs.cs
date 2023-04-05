namespace SyncTrafficLights;

public record TrafficLightSynchronizationEventArgs(TrafficLight TrafficLight, State CurrentState);