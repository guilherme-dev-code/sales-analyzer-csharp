using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Enums;
using CSVSalesPro.Services.Exceptions;

namespace CSVSalesPro.Services
{
    internal class CategoryService
    {
        public CategoryService() { }
        public Dictionary<ProductCategory, double> GetRevenueByCategory(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new CategoryServiceException("\r\nNo data to return, the list is empty.");
            }

            Dictionary<ProductCategory, double> revenueByCategory = new Dictionary<ProductCategory, double>();

            foreach (SaleItem item in saleItems)
            {
                ProductCategory category = item.Product.ProductCategory;

                double subtotal = item.CalculateSubtotal();

                if (!revenueByCategory.ContainsKey(category))
                {
                    revenueByCategory[category] = 0.0;
                }

                revenueByCategory[category] += subtotal;
            }
            return revenueByCategory;
        }

        public Dictionary<ProductCategory, int> GetQuantityByCategory(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new CategoryServiceException("\r\nNo data to return, the list is empty.");
            }

            Dictionary<ProductCategory, int> quantityByCategory = new Dictionary<ProductCategory, int>();

            foreach (SaleItem sale in saleItems)
            {
                ProductCategory category = sale.Product.ProductCategory;

                int quantity = sale.Quantity;

                if (!quantityByCategory.ContainsKey(category))
                {
                    quantityByCategory[category] = 0;
                }

                quantityByCategory[category] += quantity;
            }

            return quantityByCategory;
        }

        public ProductCategory GetBestCategory(List<SaleItem> saleItems)
        {
            Dictionary<ProductCategory, int> quantityByCategory = GetQuantityByCategory(saleItems);

            int greaterQuantity = quantityByCategory.Values.Max();

            ProductCategory bestCategory = default;

            foreach (var item in quantityByCategory)
            {
                if (item.Value == greaterQuantity)
                {
                    bestCategory = item.Key;
                }
            }

            return bestCategory;
        }

        public ProductCategory GetWorstCategory(List<SaleItem> saleItems)
        {
            Dictionary<ProductCategory, int> quantityByCategory = GetQuantityByCategory(saleItems);

            int smallerQuantity = quantityByCategory.Values.Min();

            ProductCategory worstCategory = default;

            foreach (var item in quantityByCategory)
            {
                if (item.Value == smallerQuantity)
                {
                    worstCategory = item.Key;
                }
            }

            return worstCategory;
        }

        public ProductCategory GetMostProfitableCategory(List<SaleItem> saleItems)
        {

            Dictionary<ProductCategory, double> revenueByCategory = GetRevenueByCategory(saleItems);

            double higherRevenue = revenueByCategory.Values.Max();

            ProductCategory mostProfitableCategory = default;

            foreach (var item in revenueByCategory)
            {
                if (item.Value == higherRevenue)
                {
                    mostProfitableCategory = item.Key;
                }
            }

            return mostProfitableCategory;
        }

        public ProductCategory GetLeastProfitableCategory(List<SaleItem> saleItems)
        {

            Dictionary<ProductCategory, double> revenueByCategory = GetRevenueByCategory(saleItems);

            double lowerRevenue = revenueByCategory.Values.Min();

            ProductCategory leastProfitableCategory = default;

            foreach (var item in revenueByCategory)
            {
                if (item.Value == lowerRevenue)
                {
                    leastProfitableCategory = item.Key;
                }
            }

            return leastProfitableCategory;
        }

        private bool ValidateList(List<SaleItem> saleItems)
        {
            return saleItems != null && saleItems.Count > 0;
        }
    }
}
