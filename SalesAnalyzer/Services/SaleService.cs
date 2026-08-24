using CSVSalesPro.Entities;
using CSVSalesPro.Services.Exceptions;

namespace CSVSalesPro.Services
{
    internal class SaleService
    {
        public SaleService() { }
        public double GetTotalSales(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new SaleServiceException("No data to return, the list is empty.");
            }

            double totalSale = 0.0;

            foreach (SaleItem item in saleItems)
            {
                totalSale += item.CalculateSubtotal();
            }

            return totalSale;
        }

        public int GetTotalItemsSold(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new SaleServiceException("No data to return, the list is empty.");
            }

            int itemsSold = 0;

            foreach (SaleItem item in saleItems)
            {
                itemsSold += item.Quantity;
            }

            return itemsSold;
        }

        public double GetTotalShipping(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new SaleServiceException("No data to return, the list is empty.");
            }

            double totalShipping = 0.0;

            foreach (SaleItem item in saleItems)
            {
                totalShipping += item.Product.CalculateShipping() * item.Quantity;
            }

            return totalShipping;
        }

        public double GetTotalCommission(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new SaleServiceException("No data to return, the list is empty.");
            }

            double totalCommission = 0.0;

            foreach (SaleItem item in saleItems)
            {
                totalCommission += item.Product.CalculateCommission() * item.Quantity;
            }

            return totalCommission;
        }

        private bool ValidateList(List<SaleItem> saleItems)
        {
            return saleItems != null && saleItems.Count > 0;
        }
    }
}
