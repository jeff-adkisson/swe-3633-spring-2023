namespace WackyChatConsole;

public static class Machine
{
    private static string? _machineProcess;

    public static string Name =>
        _machineProcess ??= $"{Environment.MachineName}.{Environment.UserName}.{Environment.ProcessId}";
}