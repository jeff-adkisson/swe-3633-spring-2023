namespace VoiceRecorderState;

//list of states available to the voice recorder
public enum State
{
    TerminateSimulation = -1, //special state to terminate simulation - not actually part of voice recorder
    
    Off = 1,
    On = 2,
    Play = 3,
    Record = 4,
    Erase = 5,
    Stop = 6,
    Error = 7
}