using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc_Coffee.Models;

namespace Mvc_Coffee.Controllers;

public class ManagementController : Controller
{
    private readonly ILogger<ManagementController> _logger;

    public ManagementController(ILogger<ManagementController> logger)
    {
        _logger = logger;
    }

    [Route("Management")]
    [Route("Management/Index")]

    public IActionResult Index()
    {
        return View();
    }

}