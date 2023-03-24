using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StateMachine.MVC.Models;

namespace StateMachine.MVC.Controllers.Mvc;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new FindEmailAddressesModel());
    }
    
    [HttpPost]
    public IActionResult Index(FindEmailAddressesModel model)
    {
        _logger.Log(LogLevel.Debug, "POST Received to {controller} {name} method", nameof(HomeController), nameof(Index));

        _logger.Log(LogLevel.Debug, "Text to scan: {TextToScan}", model.Text);
        var matches = Email.Finder.Find(model.Text).ToList();
        
        model.Matches = matches;
        _logger.Log(LogLevel.Debug, "Matches: {Matches}", matches);
        
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
