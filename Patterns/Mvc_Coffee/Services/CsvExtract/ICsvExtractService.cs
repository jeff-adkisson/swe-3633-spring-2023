using System.Collections.Generic;

namespace Mvc_Coffee.Services.CsvExtract;

public interface ICsvExtractService
{
    string ConvertToCsv<TModel>(IEnumerable<TModel> listToConvert);

    void WriteCsvFile<TModel>(IEnumerable<TModel> listToConvert, string fullPath);
}