using CSVSalesPro.Entities;
using CSVSalesPro.Services;

namespace CSVSalesPro.Services.Interfaces
{
    internal interface IImportService
    {
        List<Sale> ImportFile(string pathFile);
    }
}
