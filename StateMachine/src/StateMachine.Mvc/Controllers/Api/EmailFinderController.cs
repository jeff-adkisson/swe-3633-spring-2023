using Microsoft.AspNetCore.Mvc;
using StateMachine.MVC.Contracts;
using StateMachine.MVC.Email;

namespace StateMachine.MVC.Controllers.Api;

[ApiController]
public class EmailFinderController : Controller
{
    private readonly ILogger<EmailFinderController> _logger;

    public EmailFinderController(ILogger<EmailFinderController> logger)
    {
        _logger = logger;
    }

    [HttpPost("api/email/find")]
    public IActionResult FindMatches([FromBody] FindEmailAddressesRequest request)
    {
        _logger.Log(LogLevel.Debug, "POST received to API {controller} {name} method", nameof(EmailFinderController), nameof(FindMatches));

        _logger.Log(LogLevel.Debug, "Text to scan: {TextToScan}", request.Text);
        var matches = Finder.Find(request.Text);

        _logger.Log(LogLevel.Debug, "Matches: {Matches}", matches);        
        var response = new FindEmailAddressesResponse { Matches = matches};
        return Ok(response);
    }
}