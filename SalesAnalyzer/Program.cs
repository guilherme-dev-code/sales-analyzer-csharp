using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Exceptions;
using CSVSalesPro.Services;
using CSVSalesPro.Services.Exceptions;
using CSVSalesPro.Menus;
using CSVSalesPro.Utils;
using System.Globalization;

namespace CSVSalesPro
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool running = true;
            int n;

            while (running)
            {
                Console.WriteLine("SALES PRO - MAIN MENU");
                Console.WriteLine();

                Console.WriteLine("1 - ENTERPRISE");
                Console.WriteLine("2 - BRANCHES");
                Console.WriteLine("3 - IMPORT SALES");
                Console.WriteLine("4 - SALES");
                Console.WriteLine("5 - REPORTS");
                Console.WriteLine("0 - EXIT");

                Console.WriteLine();
                Console.Write("Select an option: ");
                string option = (Console.ReadLine() ?? "").Trim();

                while (!int.TryParse(option, out n))
                {
                    Console.Write("Invalid option!\nEnter with the option again: ");
                    option = (Console.ReadLine() ?? "").Trim();
                }
                switch (n)
                {
                    case (0):
                        Console.WriteLine("Thanks to use sales pro!");
                        running = false;
                        break;
                    case (1):
                        EnterpriseMenu.EnterpriseOptions();
                        Console.WriteLine();
                        break;
                    case (2):
                        BranchMenu.BranchOptions();
                        Console.WriteLine();
                        break;
                    case (3):
                        ImportMenu.ImportOptions();
                        Console.WriteLine();
                        break;
                    case (4):
                        SaleMenu.SaleOptions();
                        Console.WriteLine();
                        break;
                    case (5):
                        ReportMenu.ReportOptions();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Invalid option! Please check the options in menu!");
                        break;
                }
            }
        }
    }
}