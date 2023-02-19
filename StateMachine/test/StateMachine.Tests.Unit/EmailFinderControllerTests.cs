using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using StateMachine.MVC.Contracts;
using StateMachine.MVC.Controllers.Api;
using StateMachine.MVC.Mappers;
using Xunit.Abstractions;

namespace StateMachine.Unit.Tests;

public class EmailFinderControllerTests : TestBase
{
    private readonly EmailFinderController _sut;
    private readonly ILogger<EmailFinderController> _logger = Substitute.For<ILogger<EmailFinderController>>();

    public EmailFinderControllerTests(ITestOutputHelper console) : base(console)
    {
        _sut = new EmailFinderController(_logger);
    }

    [Fact]
    public void HomeController_ShouldFindAddresses_WhenAddressesAreSubmitted()
    {
        // Arrange
        var textToSubmit = SampleTextContainer.GetProjectDescriptionWithValidEmailAddresses();;
        var request = new FindEmailAddressesRequest()
        {
            Text = textToSubmit.Text
        };
        var expectedResponse = textToSubmit.Matches.ToEmailAddressesResponse();
        
        // Act
        var response = (OkObjectResult)_sut.FindMatches(request);
        
        // Assert
        response.Should().BeOfType<OkObjectResult>();
        response.StatusCode.Should().Be(200);
        response.Value.As<FindEmailAddressesResponse>().Should().BeEquivalentTo(expectedResponse);
    }
    
    [Fact]
    public void HomeController_ShouldNotFindAddresses_WhenNoValidAddressesAreSubmitted()
    {
        // Arrange
        var textToSubmit = SampleTextContainer.GetProjectDescriptionWithNoValidEmailAddresses();;
        var request = new FindEmailAddressesRequest()
        {
            Text = textToSubmit.Text
        };
        var expectedResponse = textToSubmit.Matches.ToEmailAddressesResponse();
        
        // Act
        var response = (OkObjectResult)_sut.FindMatches(request);
        
        // Assert
        response.Should().BeOfType<OkObjectResult>();
        response.StatusCode.Should().Be(200);
        response.Value.As<FindEmailAddressesResponse>().Should().BeEquivalentTo(expectedResponse);
    }
}