namespace StateMachine.MVC.Email;

public static class StateFactory
{
    private static readonly Dictionary<Type, IState> States = new();

    static StateFactory()
    {
        PopulateStates();
    }

    /// <summary>
    ///     Accepts type T and returns the corresponding IState
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IState Get<T>() where T : IState
    {
        return States[typeof(T)];
    }

    /// <summary>
    ///     Populate States dictionary from all states implementing IState
    ///     for fast lookup. Uses reflection to make instantiation automatic if new states are added.
    /// </summary>
    private static void PopulateStates()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IState).IsAssignableFrom(p) && p.IsClass)
            .ToList();

        foreach (var type in types) States.Add(type, (Activator.CreateInstance(type) as IState)!);
    }
}