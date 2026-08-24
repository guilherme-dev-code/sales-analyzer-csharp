using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Enums;
using ClosedXML.Excel;
using CSVSalesPro.Services.Interfaces;
using CSVSalesPro.Services.Exceptions;

namespace CSVSalesPro.Services
{
    internal class ExcelImportService : IImportService
    {
        private readonly ImportValidationService importValidation = new ImportValidationService();
        public List<Sale> ImportFile(string pathFileExcel)
        {
            List<Sale> listSale = new List<Sale>();

            try
            {
                using (XLWorkbook workbook = new XLWorkbook(pathFileExcel))
                {

                    var worksheet = workbook.Worksheet(1);

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            if (string.IsNullOrWhiteSpace(row.Cell(i).GetString()))
                            {
                                throw new ExcelImportServiceException($"Error registering cell: {row.Cell(i).Address}. The cell is empty");
                            }
                        }

                        int saleId = row.Cell(1).GetValue<int>();

                        string saleDateString = row.Cell(2).GetString();

                        DateTime saleDate = DateTime.Parse(saleDateString);

                        Sale? sale = listSale.Find(x => x.Id == saleId);

                        if (sale == null)
                        {
                            sale = new Sale(saleId, saleDate);
                            listSale.Add(sale);
                        }

                        string nameProduct = row.Cell(3).GetString();

                        string productCategory = row.Cell(4).GetString();

                        ProductCategory category = importValidation.ConvertToProductCategory(productCategory);

                        double priceProduct = row.Cell(5).GetValue<double>();

                        int quantityProduct = row.Cell(6).GetValue<int>();

                        importValidation.ValidateSaleItemData(nameProduct, productCategory, priceProduct, quantityProduct);

                        SaleItem saleItem;

                        if (category is ProductCategory.ELECTRONIC)
                        {
                            int warrantyMonths = row.Cell(7).GetValue<int>();
                            Product product = new ElectronicProduct(nameProduct, priceProduct, warrantyMonths);
                            saleItem = new SaleItem(product, quantityProduct);
                        }
                        else if (category is ProductCategory.FURNITURE)
                        {
                            string material = row.Cell(7).GetString();
                            Product product = new FurnitureProduct(nameProduct, priceProduct, material);
                            saleItem = new SaleItem(product, quantityProduct);
                        }
                        else if (category is ProductCategory.PERIPHERAL)
                        {
                            string connectionType = row.Cell(7).GetString();

                            ConnectionType connection = importValidation.ConvertToConnectionType(connectionType);

                            Product product = new PeripheralProduct(nameProduct, priceProduct, connection);
                            saleItem = new SaleItem(product, quantityProduct);
                        }
                        else
                        {
                            throw new ExcelImportServiceException("Category not supported.");
                        }

                        sale.AddSaleItem(saleItem);
                    }
                }

                return listSale;
            }
            catch (IOException e)
            {
                throw new IOException("The path file can't read!", e);
            }
        }
    }
}
