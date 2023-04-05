using SyncTrafficLights;

Console.WriteLine("STARTING... PRESS ANY KEY TO STOP");

var configurations = TrafficLightConfigurationFactory.GetConfigurations();

var signaler = new SynchronizedSignaler(configurations);

signaler.Start();

Console.ReadKey();

signaler.Stop();