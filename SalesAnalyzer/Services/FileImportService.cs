using CSVSalesPro.Entities;
using CSVSalesPro.Services.Exceptions;
using CSVSalesPro.Services.Interfaces;

namespace CSVSalesPro.Services
{
    internal class FileImportService
    {
        public List<Sale> ImportFile(string pathFile)
        {
            IImportService _importService;

            string extension = Path.GetExtension(pathFile);
            if(extension == ".txt" || extension == ".csv")
            {
                _importService = new CsvImportService();
            } else if (extension == ".xlsx")
            {
               _importService = new ExcelImportService();
            }
            else
            {
                throw new FileImportServiceException("Unsupported file format!");
            }
            return _importService.ImportFile(pathFile);
        }
    }
}
