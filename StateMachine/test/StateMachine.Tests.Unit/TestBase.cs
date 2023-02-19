using Xunit.Abstractions;

namespace StateMachine.Unit.Tests;

public abstract class TestBase
{
    protected readonly ITestOutputHelper Console;

    protected TestBase(ITestOutputHelper console)
    {
        Console = console;
    }
}