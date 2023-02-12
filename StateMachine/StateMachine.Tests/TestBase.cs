using Xunit.Abstractions;

namespace StateMachine.Tests;

public abstract class TestBase
{
    protected readonly ITestOutputHelper Console;

    protected TestBase(ITestOutputHelper console)
    {
        Console = console;
    }
}