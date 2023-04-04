namespace VoiceRecorderState;

public static class TransitionTable
{
    //lists all possible transitions by current state
    public static Dictionary<State, State[]> Rules { get; } = new()
    {
        {State.Off, new[] { State.On, State.Off, State.Error }},
        {State.On, new[] { State.Off, State.Play, State.Erase, State.Record, State.Error }},
        {State.Play, new[] { State.Stop, State.Error}},
        {State.Erase, new[] { State.Stop, State.Error}},
        {State.Record, new[] { State.Stop, State.Error}},
        {State.Stop, new[] { State.Off, State.Play, State.Erase, State.Record, State.Error}},
        {State.Error, new[] { State.Stop }}
    };
}