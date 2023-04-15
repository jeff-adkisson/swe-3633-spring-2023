using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Mvc_Coffee.Services.CsvExtract;

public class CsvExtractService : ICsvExtractService
{
    public void WriteCsvFile<TModel>(IEnumerable<TModel> listToConvert, string fullPath)
    {
        using var writer = new StreamWriter(fullPath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(listToConvert);
    }
    
    public string ConvertToCsv<TModel>(IEnumerable<TModel> listToConvert)
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(listToConvert);

        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// Add service to DI container - normally done in an extension method, but this way for simplicity
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureService(IServiceCollection services)
    {
        services.AddSingleton<ICsvExtractService, CsvExtractService>();
    }
}