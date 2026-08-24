using CSVSalesPro.Entities;
using CSVSalesPro.Services;
using CSVSalesPro.Services.Exceptions;

namespace CSVSalesPro.Menus
{
    public class ReportMenu
    {
        public static void ReportOptions()
        {
            SaleService? saleService = new SaleService();
            ProductService? productService = new ProductService();
            CategoryService? categoryService = new CategoryService();

            ReportService reportService = new ReportService(saleService, productService, categoryService);

            try
            {
                bool running = true;
                int n;

                while (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("REPORTS MENU");
                    Console.WriteLine();

                    Console.WriteLine("1 - Sales Report");
                    Console.WriteLine("2 - Product Report");
                    Console.WriteLine("3 - Category Report");
                    Console.WriteLine("4 - Financial Report");
                    Console.WriteLine("5 - Product Ranking");
                    Console.WriteLine("0 - Back to main menu");

                    Console.WriteLine();
                    Console.Write("Select an option: ");
                    string option = (Console.ReadLine() ?? "").Trim();

                    Console.WriteLine();

                    while (!int.TryParse(option, out n))
                    {
                        Console.Write("Invalid option!\nEnter with the option again: ");
                        option = (Console.ReadLine() ?? "").Trim();
                    }

                    int count = ApplicationContext.Repository.Count();

                    if (count <= 0)
                    {
                        Console.WriteLine("Not any file imported! Back to menu");
                        running = false;
                        break;
                    }

                    List<SaleItem> saleItems = ApplicationContext.Repository.GetAll();

                    switch (n)
                    {
                        case (0):
                            running = false;
                            break;

                        case (1):
                            var generateSalesReport = reportService.GenerateSalesReport(saleItems);
                            Console.WriteLine($"GENERATE SALES REPORT\n{generateSalesReport}");
                            break;

                        case (2):
                            var generateProductReport = reportService.GenerateProductReport(saleItems);
                            Console.WriteLine($"GENERATE PRODUCT REPORT\n\n{generateProductReport}");
                            break;
                        case (3):
                            var generateCategoryReport = reportService.GenerateCategoryReport(saleItems);
                            Console.WriteLine($"GENERATE CATEGORY REPORT\n\n{generateCategoryReport}");
                            break;
                        case (4):
                            var generateFinancialReport = reportService.GenerateFinancialReport(saleItems);
                            Console.WriteLine($"GENERATE FINANCIAL REPORT\n\n{generateFinancialReport}");
                            break;
                        case (5):
                            var generateRankingReport = reportService.GenerateRankingReport(saleItems);
                            Console.WriteLine("GENERATE RANKING PRODUCTS\n\n");
                            foreach (var items in generateRankingReport)
                            {
                                Console.WriteLine(items);
                            }
                            break;
                        default:
                            Console.WriteLine("Invlaid option! Please check the options in menu!");
                            break;
                    }
                }
            }
            catch (RepositoryException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SaleServiceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ProductServiceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CategoryServiceException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
