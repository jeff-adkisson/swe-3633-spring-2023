using StateMachine.MVC.Email;

namespace StateMachine.MVC.Contracts;

public class FindEmailAddressesResponse
{
    public Match[] Matches { get; init; } = default!;
}