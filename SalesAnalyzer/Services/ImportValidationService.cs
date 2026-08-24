using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Services.Exceptions;

namespace CSVSalesPro.Services
{
    internal class ImportValidationService
    {
        public void ValidateSaleItemData(string nameProduct, string categoryProduct, double priceProduct, int quantityProduct)
        {
            if (string.IsNullOrWhiteSpace(nameProduct))
            {
                throw new ImportValidationServiceException("Product name is required!\nPlease check the spreasheet!");
            }

            if (string.IsNullOrWhiteSpace(categoryProduct))
            {
                throw new ImportValidationServiceException("Product category is required!\nPlease check the spreasheet!");
            }

            if (priceProduct <= 0.0)
            {
                throw new ImportValidationServiceException("Product price cannot be less than or equal to zero!");
            }

            if (quantityProduct <= 0)
            {
                throw new ImportValidationServiceException("Product quantity cannot be less than or equal to zero!");
            }
        }

        public ProductCategory ConvertToProductCategory(string productCategory)
        {
            if (!Enum.TryParse<ProductCategory>(productCategory, true, out ProductCategory category))
            {
                throw new ImportValidationServiceException("Invalid category!\n\nVALID CATEGORIES:\n1.ELECTRONIC\n2.FURNITURE\n3.PERIPHERAL");
            }
            return category;
        }

        public ConnectionType ConvertToConnectionType(string connectionType)
        {
            if (!Enum.TryParse<ConnectionType>(connectionType, true, out ConnectionType connection))
            {
                throw new ImportValidationServiceException("Invalid connection type!\n\nVALID CONNECTIONS:\n1.USB\n2.BLUETOOTH\n3.WIRELESS");
            }
            return connection;
        }
    }
}
