using System;

namespace Mvc_Coffee.Models.DrinkMenu;

public class DrinkModelCsvLine
{
    public DrinkModelCsvLine(DrinkModel drinkModel)
    {
        Name = drinkModel.Name;
        BaseDescription = drinkModel.BaseDescription;
        BasePrice = drinkModel.BasePrice;
        
        //customizations
        CustomizationCount = drinkModel.Customizations.Count;
        var sumCost = 0.0m;
        var minCost = CustomizationCount > 0 ? decimal.MaxValue : 0.0m;
        var maxCost = CustomizationCount > 0 ? decimal.MinValue : 0.0m;
        
        for (var idx = 0; idx < CustomizationCount; idx++)
        {
            var customization = drinkModel.Customizations[idx];
            sumCost += customization.Price;
            minCost = Math.Min(minCost, customization.Price);
            maxCost = Math.Max(maxCost, customization.Price);
        }
        AvgCustomizationCost = CustomizationCount > 0 ? sumCost / CustomizationCount : 0.0m; //avoid div by zero
        MinCustomizationCost = minCost;
        MaxCustomizationCost = maxCost;
        Customizations = string.Join(", ", drinkModel.Customizations);
    }
    
    public string Name { get; init; }
    
    public string BaseDescription { get; init; }
    
    public decimal BasePrice { get; init; }
    
    public int CustomizationCount { get; init; }
    
    public decimal AvgCustomizationCost { get; init; }
    
    public decimal MinCustomizationCost { get; init; }
    
    public decimal MaxCustomizationCost { get; init; }

    public string Customizations { get; init; }
}