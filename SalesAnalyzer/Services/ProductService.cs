using CSVSalesPro.Entities;
using CSVSalesPro.Services.Exceptions;

namespace CSVSalesPro.Services
{
    internal class ProductService
    {
        public ProductService() { }
        public Dictionary<Product, int> GetQuantityByProduct(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new ProductServiceException("No data to return, the list is empty.");
            }

            Dictionary<Product, int> quantityByProduct = new Dictionary<Product, int>();

            foreach (SaleItem item in saleItems)
            {
                Product product = item.Product;

                if (!quantityByProduct.ContainsKey(product))
                {
                    quantityByProduct[product] = 0;
                }

                quantityByProduct[product] += item.Quantity;
            }

            return quantityByProduct;
        }

        public Dictionary<Product, double> GetRevenueByProduct(List<SaleItem> saleItems)
        {
            if (!ValidateList(saleItems))
            {
                throw new ProductServiceException("No data to return, the list is empty.");
            }

            Dictionary<Product, double> revenueByProduct = new Dictionary<Product, double>();

            foreach (SaleItem item in saleItems)
            {
                Product product = item.Product;
                if (!revenueByProduct.ContainsKey(product))
                {
                    revenueByProduct[product] = 0.0;
                }
                revenueByProduct[product] += item.CalculateSubtotal();
            }
            return revenueByProduct;
        }

        public Product GetBestSellingProduct(List<SaleItem> saleItems)
        {
            Dictionary<Product, int> bestSellingProduct = GetQuantityByProduct(saleItems);

            int highestQuantity = bestSellingProduct.Values.Max();

            Product product = default;

            foreach (var item in bestSellingProduct)
            {
                if (item.Value == highestQuantity)
                {
                    product = item.Key;
                }
            }

            return product;
        }

        public Product GetWorstSellingProduct(List<SaleItem> saleItems)
        {
            Dictionary<Product, int> worstSellingProduct = GetQuantityByProduct(saleItems);

            int lowestQuantity = worstSellingProduct.Values.Min();

            Product product = default;

            foreach (var item in worstSellingProduct)
            {
                if (item.Value == lowestQuantity)
                {
                    product = item.Key;
                }
            }

            return product;
        }

        public Product GetMostProfitableProduct(List<SaleItem> saleItems)
        {
            Dictionary<Product, double> mostProfitableProduct = GetRevenueByProduct(saleItems);

            double highestRevenue = mostProfitableProduct.Values.Max();

            Product product = default;

            foreach (var item in mostProfitableProduct)
            {
                if (item.Value == highestRevenue)
                {
                    product = item.Key;
                }
            }

            return product;
        }

        public Product GetLeastProfitableProduct(List<SaleItem> saleItems)
        {
            Dictionary<Product, double> leastProfitableProduct = GetRevenueByProduct(saleItems);

            double lowestRevenue = leastProfitableProduct.Values.Min();

            Product product = default;

            foreach (var item in leastProfitableProduct)
            {
                if (item.Value == lowestRevenue)
                {
                    product = item.Key;
                }
            }

            return product;
        }

        public IOrderedEnumerable<KeyValuePair<Product, int>> GetProductRanking(List<SaleItem> saleItems)
        {
            Dictionary<Product, int> productRanking = GetQuantityByProduct(saleItems);

            return productRanking.OrderByDescending(x => x.Value);
        }

        private bool ValidateList(List<SaleItem> saleItems)
        {
            return saleItems != null && saleItems.Count > 0;
        }
    }
}
