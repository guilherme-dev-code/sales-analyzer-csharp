using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    public class ImportedFile
    {
        private static int NextId = 1;
        public int Id { get; set; }

        public string FileName { get; set; }

        public DateTime ImportDate { get; set; }

        public List<Sale> Sales{ get; set; } = new List<Sale>();

        public ImportedFile() { }

        public ImportedFile(string fileName, DateTime importDate)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ImportedFileException("File name is required!");
            }

            Id = NextId++;
            FileName = fileName;
            ImportDate = importDate;
        }

        public void AddSale(Sale sale)
        {
            bool validating = ValidatingSale(sale);

            if (validating)
            {
                throw new ImportedFileException("Sale already exists in the list!");
            }
            else
            {
                Sales.Add(sale);
            }
        }

        public void RemoveSale(Sale sale)
        {
            bool validating = ValidatingSale(sale);

            if (!validating)
            {
                throw new ImportedFileException("Sale actually cannot exists int the list, please add the sale in the list!");
            }
            else
            {
                Sales.Remove(sale);
            }
        }
        public Sale SearchSale(int id)
        {
            return Sales.Find(x => x.Id == id);
        }

        public List<Sale> SearchSaleData(DateTime date)
        {
            return Sales.FindAll(x => x.SaleDate == date);
        }

        public int TotalSales()
        {
            return Sales.Count;
        }

        public override string ToString()
        {
            return $"File name: {FileName}\nDate import: {ImportDate.ToString("dd-MM-yyyy")}";
        }

        private bool ValidatingSale(Sale sale)
        {
            sale = Sales.Find(x => x.Id == sale.Id);

            if(sale != null)
            {
                return true;
            }
            return false;
        }

    }
}
