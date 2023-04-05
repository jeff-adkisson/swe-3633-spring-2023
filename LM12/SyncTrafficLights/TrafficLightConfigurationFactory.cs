namespace SyncTrafficLights;

public static class TrafficLightConfigurationFactory
{
    public static List<TrafficLightConfiguration> GetConfigurations()
    {
        var configurations = new List<TrafficLightConfiguration>();
        var trafficLights = StateTransitions.GetAllTrafficLightTransitions();
        foreach (var trafficLight in trafficLights)
        {
            configurations.Add(GetConfiguration(trafficLight));
        }
        return configurations;
    }
    
    private static TrafficLightConfiguration GetConfiguration(
        KeyValuePair<TrafficLight, List<StateTransition>> configuration)
    {
        var trafficLight = configuration.Key;
        
        var transitions = configuration.Value;

        var initialState = transitions.Single(t 
            => t.IsInitialState).CurrentState;
        
        var transitionDict = 
            transitions.ToDictionary(key => key.CurrentState, value => value);

        return new TrafficLightConfiguration(trafficLight, initialState, transitionDict);
    }
}