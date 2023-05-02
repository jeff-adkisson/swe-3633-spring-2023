using System.Text.RegularExpressions;
using FluentAssertions;
using StateMachine.MVC.Email;
using Xunit.Abstractions;
using Match = StateMachine.MVC.Email.Match;

namespace StateMachine.Unit.Tests;

public partial class FinderTests : TestBase
{
    public FinderTests(ITestOutputHelper console) : base(console)
    {
    }

    [Fact]
    public void Finder_ShouldFindAddresses_WhenTextBlockContainsValidEmailAddresses()
    {
        //Arrange
        var (text, expectedMatches) = SampleTextContainer.GetProjectDescriptionWithValidEmailAddresses();

        //Act
        var actualMatches = Finder.Find(text);
        ShowMatches(actualMatches);

        //Assert
        actualMatches.Should().HaveCount(expectedMatches.Length);

        for (var i = 0; i < actualMatches.Length; i++)
        {
            expectedMatches[i].Should().BeEquivalentTo(actualMatches[i]);
            actualMatches[i].EmailAddress.Should().MatchRegex(ValidEmailAddressRegex());
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Finder_ShouldFindNoAddresses_WhenTextIsNullOrEmpty(string? text)
    {
        // Act
        var actualMatches = Finder.Find(text);
        actualMatches.Should().BeEmpty();
    }
    
    [Fact]
    public void Find_ShouldFindNoAddresses_WhenTextBlockContainsNoValidEmailAddresses()
    {
        //Arrange
        var (text, expectedMatches) = SampleTextContainer.GetProjectDescriptionWithNoValidEmailAddresses();

        //Act
        var actualMatches = Finder.Find(text);

        //Assert
        actualMatches.Should().BeEmpty();
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