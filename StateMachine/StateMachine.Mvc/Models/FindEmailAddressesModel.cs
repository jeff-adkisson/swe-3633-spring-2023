using StateMachine.MVC.Email;

namespace StateMachine.MVC.Models;

public class FindEmailAddressesModel
{
    public string? TextToScan { get; set; } =
        """
        Valid Examples
        1. Jeff-Adkisson@KSU.edu 
        2. j.adkisson@KSU.edu
        3. JeffAdkisson@KSU.university

        Invalid Examples
        1. Jeff_Adkisson@KSU.com [underscore not allowed]
        2. Jeff@KSU [top level domain is required]
        3. Jeff@KSU.u [top level domain must be 2-10 letters, alphanumeric]
        4. JeffAdkisson@KSU.universitymail [top level domain must be 2-10 letters, alphanumeric]
        5. JeffAdkisson@K_S_U.university [underscore not allowed]
        """;

    public List<Match>? Matches { get; set; }

    public override string ToString()
    {
        if (Matches != null && Matches.Any())
        {
            return $"Matches: {string.Join(", ", Matches)}";
        }
        return "No Matches";
    }
}