using CSVSalesPro.Entities.Exceptions;
using CSVSalesPro.Entities.Enums;

namespace CSVSalesPro.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }

        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        public Sale() { }

        public Sale(int id, DateTime saleDate)
        {
            Id = id;
            SaleDate = saleDate;
        }

        public void AddSaleItem(SaleItem saleItem)
        {
            bool validating = ValidatingSaleItem(saleItem);

            if (validating)
            {
                throw new SaleException("The item actualy already exists in the list!");
            }
            else
            {
                SaleItems.Add(saleItem);
            }
        }

        public void RemoveSaleItem(SaleItem saleItem)
        {
            bool validating = ValidatingSaleItem(saleItem);

            if (!validating)
            {
                throw new SaleException("The item actualy cannot be exist in the list! Please add the item in the list!");
            }
            else
            {
                SaleItems.Remove(saleItem);
            }
        }

        public SaleItem SearchSaleItem(int id)
        {
            return SaleItems.Find(x => x.Id == id);
        }

        public List<SaleItem> SearchSaleItemProductName(string name)
        {
            return SaleItems.FindAll(x => x.Product.Name == name);
        }

        public List<SaleItem> SearchSaleItemProductCategory(ProductCategory productCategory)
        {
            return SaleItems.FindAll(x => x.Product.ProductCategory == productCategory);
        }

        public double CalculateTotalSale()
        {
            double sum = 0.0;

            foreach(var i in SaleItems)
            {
                sum += i.CalculateSubtotal();
            }

            return sum;
        }

        public override string ToString()
        {
            return $"Id sale: {Id}\nDate sale: {SaleDate.ToString("dd-MM-yyyy")}";
        }


        private bool ValidatingSaleItem(SaleItem saleItem)
        {
            saleItem = SaleItems.Find(x => x.Id == saleItem.Id);
            if(saleItem != null)
            {
                return true;
            }
            return false;
        }
    }
}
