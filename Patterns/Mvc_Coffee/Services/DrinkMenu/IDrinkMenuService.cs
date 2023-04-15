using System.Collections.Generic;
using Mvc_Coffee.Models.DrinkMenu;

namespace Mvc_Coffee.Services.DrinkMenu;

public interface IDrinkMenuService
{
    public IReadOnlyList<DrinkModel> Drinks { get; }

    List<DrinkModelCsvLine> ConvertToCsvModel();
}