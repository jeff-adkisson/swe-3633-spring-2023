using StateMachine.MVC.Contracts;
using StateMachine.MVC.Email;

namespace StateMachine.MVC.Mappers;

public static class MatchesMapper
{
    public static FindEmailAddressesResponse ToEmailAddressesResponse(this Match[] matches)
    {
        return new FindEmailAddressesResponse
        {
            Matches = matches
        };
    }
}