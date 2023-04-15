using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Mvc_Coffee.Services.CsvExtract;
using Mvc_Coffee.Services.DrinkMenu;

namespace Mvc_Coffee.Controllers;

public class CsvExtractController : Controller
{
    private readonly ICsvExtractService _csvExtractService;
    private readonly IDrinkMenuService _drinkMenuService;
    private readonly ILogger<CsvExtractController> _logger;

    public CsvExtractController(ILogger<CsvExtractController> logger, IDrinkMenuService drinkMenuService,
        ICsvExtractService csvExtractService)
    {
        _logger = logger;
        _drinkMenuService = drinkMenuService;
        _csvExtractService = csvExtractService;
    }

    [Route("CsvExtract/DrinkMenu")]
    public IActionResult DrinkMenu()
    {
        //convert drink menu into flat format better suited for CSV (drink menu is hierarchical)
        var drinkCsvData = _drinkMenuService.ConvertToCsvModel();

        //create csv file in memory, then return to call with unique filename
        var csv = _csvExtractService.ConvertToCsv(drinkCsvData);
        var bytes = Encoding.UTF8.GetBytes(csv);
;        return new FileContentResult(bytes, "text/csv")
        {
            FileDownloadName = $"drink_menu_{DateTime.Now:yyyy-MM-dd-HH-mm-s}.csv"
        };
    }
}