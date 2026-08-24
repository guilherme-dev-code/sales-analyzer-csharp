using System.Globalization;
using CSVSalesPro.Entities;


namespace CSVSalesPro.Services
{
    internal class ReportService
    {
        private readonly SaleService _saleService;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ReportService(SaleService saleService, ProductService productService, CategoryService categoryService)
        {
            _saleService = saleService;
            _productService = productService;
            _categoryService = categoryService;
        }
        public string GenerateSalesReport(List<SaleItem> saleItems)
        {
            return $"SALES REPORT\n\nTotal sales: {_saleService.GetTotalSales(saleItems).ToString("F2", CultureInfo.InvariantCulture)}\nItems Sold: {_saleService.GetTotalItemsSold(saleItems)}\nTotal Shipping: {_saleService.GetTotalShipping(saleItems).ToString("F2", CultureInfo.InvariantCulture)}\nTotal Commission: {_saleService.GetTotalCommission(saleItems).ToString("F2", CultureInfo.InvariantCulture)}";
        }

        public string GenerateCategoryReport(List<SaleItem> saleItems)
        {
            return $"CATEGORY REPORT\n\nBest Category: {_categoryService.GetBestCategory(saleItems)}\n\nWorst Category: {_categoryService.GetWorstCategory(saleItems)}\n\nMost Profitable Category: {_categoryService.GetMostProfitableCategory(saleItems)}\n\nLeast Profitable Category: {_categoryService.GetLeastProfitableCategory(saleItems)}";
        }

        public string GenerateProductReport(List<SaleItem> saleItems)
        {
            return $"PRODUCT REPORT\n\nBest Selling Product: {_productService.GetBestSellingProduct(saleItems)}\n\nWorst Selling Product: {_productService.GetWorstSellingProduct(saleItems)}\n\nMost Profitable Product: {_productService.GetMostProfitableProduct(saleItems)}\n\nLeast Profitable Product: {_productService.GetLeastProfitableProduct(saleItems)}";
        }

        public List<string> GenerateRankingReport(List<SaleItem> saleItems)
        {
            List<string> listRanking = new List<string>();

            int position = 1;

            var ranking = _productService.GetProductRanking(saleItems);

            foreach (var item in ranking)
            {
                listRanking.Add($"{position++}° {item.Key} - {item.Value}");
            }
            return listRanking;
        }

        public string GenerateFinancialReport(List<SaleItem> saleItems)
        {

            return $"FINANCIAL REPORT\n\nRevenue: {_saleService.GetTotalSales(saleItems).ToString("F2", CultureInfo.InvariantCulture)}\n\nShipping: {_saleService.GetTotalShipping(saleItems).ToString("F2", CultureInfo.InvariantCulture)}\n\nCommission: {_saleService.GetTotalCommission(saleItems).ToString("F2", CultureInfo.InvariantCulture)}\n\nMost Profitable Category: {_categoryService.GetMostProfitableCategory(saleItems)}\n\nMost Profitable Product: {_productService.GetMostProfitableProduct(saleItems)}";
        }
    }
}
