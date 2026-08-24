using CSVSalesPro.Entities;
using CSVSalesPro.Utils;
using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Menus
{
    public class BranchMenu
    {
        public static void BranchOptions()
        {
            Branch? branch;

            try
            {
                bool running = true;
                int n;

                while (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("BRANCHES");
                    Console.WriteLine();

                    Console.WriteLine("1 - Register branches");
                    Console.WriteLine("2 - Remove branch");
                    Console.WriteLine("3 - Search branch by id");
                    Console.WriteLine("4 - Search branch by name");
                    Console.WriteLine("5 - List branches");
                    Console.WriteLine("0 - Back to main menu");
                    Console.WriteLine();


                    if (ApplicationContext.Enterprise == null)
                    {
                        Console.WriteLine("No enterprise registered.\nPlease register an enterprise first.");
                        running = false;
                        break;
                    }

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

                            branch = RegisterBranch();

                            ApplicationContext.Enterprise.AddBranch(branch);

                            Console.WriteLine("Branch registered successfully!");

                            break;
                        case (2):
                            bool success = RemoveBranch();
                            if (success == true)
                            {
                                Console.WriteLine("Branch removed successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Branch not found! Check if the branch exists in list!");
                            }
                            break;
                        case (3):
                            branch = SearchBranchById();
                            if (branch != null)
                            {
                                Console.WriteLine($"Branch found!\n\n{branch}");
                            }
                            else
                            {
                                Console.WriteLine("Branch not found! Please check if the branch exists in list!");
                            }
                            break;
                        case (4):
                            branch = SearchBranch.SearchBranchByName();
                            if (branch != null)
                            {
                                Console.WriteLine($"Branch found!\n\n{branch}");
                            }
                            else
                            {
                                Console.WriteLine("Branch not found! Please check if the branch exists in list!");
                            }
                            break;
                        case (5):

                            List<Branch> branchList = ApplicationContext.Enterprise.Branches;
                            if (branchList.Count == 0)
                            {
                                Console.WriteLine("List is empty!");
                            }
                            else
                            {
                                foreach (var item in branchList)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please check the options in menu!");
                            break;
                    }
                }
            }
            catch (EnterpriseException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (BranchException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (AddressException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static Branch RegisterBranch()
        {
            Console.Write("Branch name: ");
            string branchName = (Console.ReadLine() ?? "").ToUpper().Trim();

            Address address = RegisterAddress();

            return new Branch(branchName, address);
        }

        private static Address RegisterAddress()
        {
            int number;

            Console.Write("Street: ");
            string street = (Console.ReadLine() ?? "").ToUpper().Trim();

            Console.Write("Number: ");
            string numberBranch = (Console.ReadLine() ?? "").Trim();

            while (!int.TryParse(numberBranch, out number))
            {
                Console.Write("Invalid number!\nEnter the number again: ");
                numberBranch = (Console.ReadLine() ?? "").Trim();
            }

            Console.Write("Neighborhood: ");
            string neighborhood = (Console.ReadLine() ?? "").ToUpper().Trim();

            Console.Write("City: ");
            string city = (Console.ReadLine() ?? "").ToUpper().Trim();

            Console.Write("State: ");
            string state = (Console.ReadLine() ?? "").ToUpper().Trim();

            Console.Write("CEP: ");
            string cep = (Console.ReadLine() ?? "").Trim();

            return new Address(street, number, neighborhood, city, state, cep);
        }

        private static bool RemoveBranch()
        {
            Console.Write("Branch name: ");
            string branchName = (Console.ReadLine() ?? "").ToUpper().Trim();

            Branch? branch = ApplicationContext.Enterprise!.SearchNameBranch(branchName);

            if (branch != null)
            {
                ApplicationContext.Enterprise.RemoveBranch(branch);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Branch? SearchBranchById()
        {
            int n;

            Console.WriteLine("Enter with the Id branch:");
            string idBranch = (Console.ReadLine() ?? "").Trim();
            while (!int.TryParse(idBranch, out n))
            {
                Console.Write("Invalid ID!\nEnter with the Id branch again: ");
                idBranch = (Console.ReadLine() ?? "").Trim();
            }
            Branch? branch = ApplicationContext.Enterprise!.SearchIdBranch(n);

            return branch;
        }
    }
}
