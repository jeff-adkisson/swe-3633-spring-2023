using System.Text.RegularExpressions;
using StateMachine.MVC.Email;
using Xunit.Abstractions;
using Match = StateMachine.MVC.Email.Match;

namespace StateMachine.Tests;

public partial class StateMachineTests : TestBase
{
    public StateMachineTests(ITestOutputHelper console) : base(console)
    {
    }

    [Fact]
    public void TestFindingEmailsInProjectDescription()
    {
        //Arrange
        var (text, expectedMatches) = SampleTextContainer.GetProjectDescription();

        //Act
        var actualMatches = Finder.Find(text);
        ShowMatches(actualMatches);

        //Assert
        Assert.Equal(expectedMatches.Length, actualMatches.Length);

        for (var i = 0; i < actualMatches.Length; i++)
        {
            Assert.Equal(expectedMatches[i], actualMatches[i]);
            Assert.True(MatchesRegex(actualMatches[i].EmailAddress));
        }
        
    }
    
    private bool MatchesRegex(string possibleEmailAddress)
    {
        var regex = ValidEmailAddressRegex();
        var match = regex.Match(possibleEmailAddress);
        return match.Success;
    }

    private void ShowMatches(IReadOnlyList<Match> matches)
    {
        if (matches.Any())
        {
            Console.WriteLine($"Found {matches.Count} Email Addresses:");
            for (var i = 0; i < matches.Count; i++)
                Console.WriteLine($"{i + 1}. {matches[i].EmailAddress}, {matches[i].StartIndex}");
        }
        else
        {
            Console.WriteLine("No valid email addresses found in text");
        }
    }

    [GeneratedRegex("\\b([a-zA-Z0-9\\\\.-]+)@([a-zA-Z0-9]+)\\.([a-zA-Z0-9]{2,10})\\b")]
    private static partial Regex ValidEmailAddressRegex();
}