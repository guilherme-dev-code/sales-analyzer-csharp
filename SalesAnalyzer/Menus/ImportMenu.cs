using CSVSalesPro.Entities;
using CSVSalesPro.Services;
using CSVSalesPro.Services.Exceptions;
using CSVSalesPro.Utils;

namespace CSVSalesPro.Menus
{
    public class ImportMenu
    {
        public static void ImportOptions()
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
                    Console.WriteLine("IMPORT SALES");
                    Console.WriteLine();

                    Console.WriteLine("1 - Import File");
                    Console.WriteLine("2 - List Imported Files");
                    Console.WriteLine("3 - Search Imported File");
                    Console.WriteLine("4 - Remove file");
                    Console.WriteLine("0 - Back to main menu");
                    Console.WriteLine();

                    Console.Write("Select an option:");
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

                            if (branch == null)
                            {
                                Console.WriteLine("Branch not found!\nPlease check list branches!");
                                break;
                            }

                            string path = SelectFileImport.SelectFile();

                            if (!string.IsNullOrWhiteSpace(path))
                            {

                                string fileName = Path.GetFileName(path);

                                FileImportService importFile = new FileImportService();

                                List<Sale> sales = importFile.ImportFile(path);

                                importedFile = new ImportedFile(fileName, DateTime.Now);

                                foreach (var sale in sales)
                                {
                                    importedFile.AddSale(sale);

                                    foreach (var saleItem in sale.SaleItems)
                                    {
                                        ApplicationContext.Repository.Add(saleItem);
                                    }
                                }

                                branch.AddFile(importedFile);

                                Console.WriteLine("File imported successfully!");
                            }
                            else
                            {
                                Console.WriteLine("File path not found");
                            }

                            break;

                        case (2):

                            branch = SearchBranch.SearchBranchByName();

                            if (branch != null)
                            {
                                List<ImportedFile> filesImported = branch.ImportedFiles;

                                if (filesImported.Count > 0)
                                {
                                    foreach (var importFile in filesImported)
                                    {
                                        Console.WriteLine(importFile);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("List is empty!");
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
                                Console.Write("Enter id file imported: ");
                                string idFile = (Console.ReadLine() ?? "").Trim();

                                while (!int.TryParse(idFile, out n))
                                {
                                    Console.Write("ID file invalid!\nPlease enter with ID file again: ");
                                    idFile = (Console.ReadLine() ?? "").Trim();
                                }

                                importedFile = branch.SearchFile(n);

                                if (importedFile != null)
                                {
                                    Console.WriteLine($"File find successfully!\n{importedFile}");
                                }
                                else
                                {
                                    Console.WriteLine("File not found!\nPlease check the list file!");
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
                                Console.Write("Enter id file imported: ");
                                string idFile = (Console.ReadLine() ?? "").Trim();

                                while (!int.TryParse(idFile, out n))
                                {
                                    Console.Write("ID file invalid!\nPlease enter with ID file again: ");
                                    idFile = (Console.ReadLine() ?? "").Trim();
                                }

                                importedFile = branch.SearchFile(n);

                                if (importedFile != null)
                                {
                                    var listSaleItemsFile = importedFile.Sales;

                                    foreach (var saleItemsFile in listSaleItemsFile)
                                    {
                                        var saleItems = saleItemsFile.SaleItems;

                                        foreach (var items in saleItems)
                                        {
                                            ApplicationContext.Repository.Remove(items);
                                        }
                                    }
                                    branch.RemoveFile(importedFile);

                                    Console.WriteLine($"File removed successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("File not found!\nPlease check list file");
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
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileImportServiceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (CsvImportServiceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ExcelImportServiceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
