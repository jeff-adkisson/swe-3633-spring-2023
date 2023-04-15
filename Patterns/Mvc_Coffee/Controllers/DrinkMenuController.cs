using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc_Coffee.Services.DrinkMenu;

namespace Mvc_Coffee.Controllers;

public class DrinkMenuController : Controller
{
    private readonly IDrinkMenuService _drinkMenuService;
    private readonly ILogger<DrinkMenuController> _logger;

    public DrinkMenuController(ILogger<DrinkMenuController> logger, IDrinkMenuService drinkMenuService)
    {
        _logger = logger;
        _drinkMenuService = drinkMenuService;
    }

    [Route("DrinkMenu")]
    [Route("DrinkMenu/Index")]
    public IActionResult Index()
    {
        return View(_drinkMenuService.Drinks);
    }
    
    [Route("DrinkMenu/Customize/{id}")]
    public IActionResult Customize(string id)
    {
        var selectedDrink = _drinkMenuService.Drinks
            .FirstOrDefault(d => d.Url.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        if (selectedDrink == null) return RedirectToAction("Index", "DrinkMenu");
        
        return View(selectedDrink);
    }
}