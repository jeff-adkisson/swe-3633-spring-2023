namespace SyncTrafficLights;

public sealed class SynchronizedSignaler
{
    private readonly List<TrafficLightStateMachine> _trafficLights = new();

    public SynchronizedSignaler(List<TrafficLightConfiguration> trafficLightConfigurations)
    {
        foreach (var trafficLightConfiguration in trafficLightConfigurations)
        {
            var trafficLight = new TrafficLightStateMachine(trafficLightConfiguration);
            trafficLight.SynchronizeTrafficLights += OnSynchronizeTrafficLights;
            _trafficLights.Add(trafficLight);
        }
    }

    public void Start()
    {
        foreach(var trafficLight in _trafficLights)
        {
            trafficLight.Start();
        }
    }

    public void Stop()
    {
        foreach(var trafficLight in _trafficLights)
        {
            trafficLight.Stop();
        }
    }

    private void OnSynchronizeTrafficLights(object? sender, TrafficLightSynchronizationEventArgs args)
    {
        foreach (var trafficLightStateMachine in _trafficLights)
        {
            trafficLightStateMachine.OnSynchronize(args);
        }
    }
}