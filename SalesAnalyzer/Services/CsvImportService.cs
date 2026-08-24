using System.Globalization;
using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Services.Exceptions;
using CSVSalesPro.Services.Interfaces;

namespace CSVSalesPro.Services
{
    internal class CsvImportService : IImportService
    {
        private readonly ImportValidationService importValidation = new ImportValidationService();
        public List<Sale> ImportFile(string pathFileCsv)
        {
            List<Sale> listSale = new List<Sale>();
            int count = 0;

            try
            {
                using (StreamReader sr = File.OpenText(pathFileCsv))
                {
                    while (!sr.EndOfStream)
                    {
                        string? line = sr.ReadLine();

                        count++;

                        if (string.IsNullOrWhiteSpace(line))
                        {
                            throw new CsvImportServiceException($"Error registering line: {count}. The line is empty.");
                        }

                        string[] vet = line.Split(";");

                        if (vet.Length != 7)
                        {
                            throw new CsvImportServiceException("The file must have 7 columns, representing:\n\n1.ID SALE\n2.DATE SALE\n3.PRODUCT NAME\n4.PRODUCT CATEGORY\n5.PRODUCT PRICE\n6.PRODUCT QUANTITY\n\nThe seven column shoul de filled according to the product category!");
                        }
                        else
                        {
                            int saleId = int.Parse(vet[0].Trim());

                            DateTime saleDate = DateTime.Parse(vet[1].Trim());

                            Sale? sale = listSale.Find(x => x.Id == saleId);

                            if (sale == null)
                            {
                                sale = new Sale(saleId, saleDate);
                                listSale.Add(sale);
                            }


                            string nameProduct = vet[2].Trim();

                            string productCategory = vet[3].Trim();

                            double priceProduct = double.Parse(vet[4].Trim(), CultureInfo.InvariantCulture);

                            int quantityProduct = int.Parse(vet[5].Trim());

                            importValidation.ValidateSaleItemData(nameProduct, productCategory, priceProduct, quantityProduct);

                            ProductCategory category = importValidation.ConvertToProductCategory(productCategory);

                            SaleItem saleItem;

                            if (category is ProductCategory.ELECTRONIC)
                            {
                                int warrantyMonths = int.Parse(vet[6].Trim());
                                Product product = new ElectronicProduct(nameProduct, priceProduct, warrantyMonths);
                                saleItem = new SaleItem(product, quantityProduct);

                            }
                            else if (category is ProductCategory.FURNITURE)
                            {
                                string material = vet[6].Trim();
                                Product product = new FurnitureProduct(nameProduct, priceProduct, material);
                                saleItem = new SaleItem(product, quantityProduct);
                            }
                            else if (category is ProductCategory.PERIPHERAL)
                            {
                                string connectionType = vet[6].Trim();

                                ConnectionType connection = importValidation.ConvertToConnectionType(connectionType);

                                Product product = new PeripheralProduct(nameProduct, priceProduct, connection);
                                saleItem = new SaleItem(product, quantityProduct);
                            }
                            else
                            {
                                throw new CsvImportServiceException("Category not supported.");
                            }
                            sale.AddSaleItem(saleItem);
                        }
                    }
                }
                return listSale;
            }
            catch (FormatException e)
            {
                throw new FormatException("Invalid input format", e);
            }
            catch (IOException e)
            {
                throw new IOException("The path file can't read!", e);
            }
        }
    }
}
