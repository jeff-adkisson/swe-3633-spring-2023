using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mvc_Coffee.Models.DrinkMenu;

public class DrinkModel
{
    private List<Customization> _customizations;
    public string Name { get; set; }
    public string BaseDescription { get; set; }

    public string Image { get; set; }

    public decimal BasePrice { get; set; }
    public string Url => Regex.Replace(Name.Trim(), "[^a-zA-Z0-9]", "-");

    public List<Customization> Customizations
    {
        get => _customizations;
        set => _customizations = value ?? new List<Customization>();
    }

    public override string ToString()
    {
        return $"{Name}: {BasePrice:C2}";
    }
}