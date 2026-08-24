using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Exceptions;
using CSVSalesPro.Utils;
using System.Globalization;

namespace CSVSalesPro.Menus
{
    public class SaleMenu
    {
        public static void SaleOptions()
        {
            try
            {
                Branch? branch;
                ImportedFile? importedFile;

                bool running = true;
                int n;

                while (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("SALES MENU");
                    Console.WriteLine();

                    Console.WriteLine("1 - List Sales");
                    Console.WriteLine("2 - Search Sale");
                    Console.WriteLine("3 - Remove Sale");
                    Console.WriteLine("4 - View Sale Details");
                    Console.WriteLine("0 - Back to main menu");
                    Console.WriteLine();

                    Console.Write("Selected an option: ");
                    string option = (Console.ReadLine() ?? "").Trim();
                    Console.WriteLine();

                    while (!int.TryParse(option, out n))
                    {
                        Console.Write("Invalid option!\nEnter with the option again: ");
                        option = (Console.ReadLine() ?? "").Trim();
                    }

                    switch (n)
                    {
                        case (0):
                            running = false;
                            break;
                        case (1):

                            branch = SearchBranch.SearchBranchByName();

                            if (branch != null)
                            {
                                Console.Write("Enter File ID: ");
                                string idFile = (Console.ReadLine() ?? "").Trim();

                                while (!int.TryParse(idFile, out n))
                                {
                                    Console.Write("ID file invalid!\nPlease enter with ID file again: ");
                                    idFile = (Console.ReadLine() ?? "").Trim();
                                }

                                importedFile = branch.SearchFile(n);

                                if (importedFile != null)
                                {
                                    foreach (var sales in importedFile.Sales)
                                    {
                                        Console.WriteLine(sales);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("File not found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Branch not located.\nPlease check the list of branches.");
                            }

                            break;

                        case (2):

                            branch = SearchBranch.SearchBranchByName();

                            if (branch != null)
                            {
                                Console.Write("Enter File ID: ");
                                string idFile = (Console.ReadLine() ?? "").Trim();

                                while (!int.TryParse(idFile, out n))
                                {
                                    Console.Write("ID file invalid!\nPlease enter with ID file again: ");
                                    idFile = (Console.ReadLine() ?? "").Trim();
                                }

                                importedFile = branch.SearchFile(n);

                                if (importedFile != null)
                                {
                                    Console.Write("Enter Sale ID: ");
                                    string idSale = (Console.ReadLine() ?? "").Trim();

                                    while (!int.TryParse(idSale, out n))
                                    {
                                        Console.Write("ID sale invalid!\nPlease enter with ID sale again: ");
                                        idSale = (Console.ReadLine() ?? "").Trim();
                                    }

                                    Sale? sale = importedFile.SearchSale(n);

                                    if (sale != null)
                                    {
                                        Console.WriteLine(sale);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sale not found!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("File not found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Branch not located.\nPlease check the list of branches.");
                            }

                            break;

                        case (3):


                            branch = SearchBranch.SearchBranchByName();

                            if (branch != null)
                            {
                                Console.Write("Enter File ID: ");
                                string idFile = (Console.ReadLine() ?? "").Trim();

                                while (!int.TryParse(idFile, out n))
                                {
                                    Console.Write("ID file invalid!\nPlease enter with ID file again: ");
                                    idFile = (Console.ReadLine() ?? "").Trim();
                                }

                                importedFile = branch.SearchFile(n);

                                if (importedFile != null)
                                {
                                    Console.Write("Enter Sale ID: ");
                                    string idSale = (Console.ReadLine() ?? "").Trim();

                                    while (!int.TryParse(idSale, out n))
                                    {
                                        Console.Write("ID sale invalid!\nPlease enter with ID sale again: ");
                                        idSale = (Console.ReadLine() ?? "").Trim();
                                    }

                                    Sale? sale = importedFile.SearchSale(n);

                                    if (sale != null)
                                    {
                                        foreach (var item in sale.SaleItems)
                                        {
                                            ApplicationContext.Repository.Remove(item);
                                        }

                                        importedFile.RemoveSale(sale);
                                        Console.WriteLine("Remove sale successfully!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sale not found!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("File not found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Branch not located.\nPlease check the list of branches.");
                            }

                            break;

                        case (4):


                            branch = SearchBranch.SearchBranchByName();

                            if (branch != null)
                            {
                                Console.Write("Enter File ID: ");
                                string idFile = (Console.ReadLine() ?? "").Trim();

                                while (!int.TryParse(idFile, out n))
                                {
                                    Console.Write("ID file invalid!\nPlease enter with ID file again: ");
                                    idFile = (Console.ReadLine() ?? "").Trim();
                                }

                                importedFile = branch.SearchFile(n);

                                if (importedFile != null)
                                {
                                    Console.Write("Enter Sale ID: ");
                                    string idSale = (Console.ReadLine() ?? "").Trim();

                                    while (!int.TryParse(idSale, out n))
                                    {
                                        Console.Write("ID sale invalid!\nPlease enter with ID sale again: ");
                                        idSale = (Console.ReadLine() ?? "").Trim();
                                    }

                                    Sale? sale = importedFile.SearchSale(n);

                                    if (sale != null)
                                    {
                                        Console.WriteLine("Sale details: ");
                                        Console.WriteLine();

                                        Console.WriteLine(sale);

                                        foreach (var sales in sale.SaleItems)
                                        {
                                            Console.WriteLine(sales);
                                            Console.WriteLine();
                                        }

                                        Console.WriteLine($"Total sale: {sale.CalculateTotalSale().ToString("F2", CultureInfo.InvariantCulture)}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sale not found!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("File not found!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Branch not located.\nPlease check the list of branches.");
                            }

                            break;

                        default:
                            Console.WriteLine("Invalid option! Please check the options in menu!");
                            break;
                    }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (BranchException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ImportedFileException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (SaleException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ProductException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ElectronicProductException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FurnitureProductException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
