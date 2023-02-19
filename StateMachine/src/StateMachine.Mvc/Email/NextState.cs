namespace StateMachine.MVC.Email;

public static class NextState
{
    private static readonly Dictionary<Type, IState> States = new();

    static NextState()
    {
        PopulateStates();
    }

    /// <summary>
    ///     Accepts type T and returns the corresponding IState
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IState GetNextState<T>(this IState _) where T : IState
    {
        return States[typeof(T)];
    }

    /// <summary>
    /// Returns the starting state
    /// </summary>
    /// <returns></returns>
    public static IState Start() => States[typeof(StateStartOfWord)];
    
    /// <summary>
    /// Returns true if <see cref="StateComplete"/> has been reached
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static bool IsComplete(this IState state) => 
        state.GetType() == typeof(StateComplete);

    /// <summary>
    ///     Populate States dictionary from all states implementing IState
    ///     for fast lookup. 
    /// </summary>
    private static void PopulateStates()
    {
        States.Add(typeof(StateAddEmailAddress), new StateAddEmailAddress());
        States.Add(typeof(StateAdvanceToNextWord), new StateAdvanceToNextWord()); 
        States.Add(typeof(StateCaptureDomain), new StateCaptureDomain());
        States.Add(typeof(StateCaptureName), new StateCaptureName());
        States.Add(typeof(StateCaptureTopLevelDomain), new StateCaptureTopLevelDomain());
        States.Add(typeof(StateStartOfWord), new StateStartOfWord());
        States.Add(typeof(StateComplete), new StateComplete());
    }
}