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
        var matches = Finder.Find(request.Text);
        var response = new FindEmailAddressesResponse { Matches = matches};
        return Ok(response);
    }
}