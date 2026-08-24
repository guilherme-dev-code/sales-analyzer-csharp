using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Exceptions;
using CSVSalesPro.Services;
using CSVSalesPro.Services.Exceptions;
using System.Globalization;

namespace CSVSalesPro
{
    public class ApplicationContext
    {
        public static Enterprise? Enterprise { get; set; }
        public static RepositoryService<SaleItem> Repository { get; } = new RepositoryService<SaleItem>();
    }
}
