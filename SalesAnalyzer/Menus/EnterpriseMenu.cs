using CSVSalesPro.Entities;
using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Menus
{
    internal class EnterpriseMenu
    {
        public static void EnterpriseOptions()
        {
            bool running = true;
            int n;

            try
            {
                while (running)
                {
                    Console.WriteLine();
                    Console.WriteLine("ENTERPRISE");
                    Console.WriteLine();

                    Console.WriteLine("1 - Register Enterprise");
                    Console.WriteLine("2 - View Enterprise");
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

                    switch (n)
                    {
                        case 0:
                            running = false;
                            break;
                        case 1:
                            if (ApplicationContext.Enterprise == null)
                            {
                                var enterprise = ApplicationContext.Enterprise = RegisterEnterprise();
                                if (enterprise != null)
                                {
                                    Console.WriteLine("Enterprise register successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Error");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Already exists an enterprise registered!\n{ApplicationContext.Enterprise}");
                            }
                            break;
                        case 2:
                            if (ApplicationContext.Enterprise != null)
                            {
                                Console.WriteLine(ApplicationContext.Enterprise);
                            }
                            else
                            {
                                Console.WriteLine("No enterprise has been registered yet");
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
        }

        private static Enterprise RegisterEnterprise()
        {
            Console.WriteLine();
            Console.Write("Social Reason: ");
            string socialReason = (Console.ReadLine() ?? "").ToUpper().Trim();
            Console.Write("CNPJ: ");
            string cnpj = (Console.ReadLine() ?? "").ToUpper().Trim();

            return new Enterprise(socialReason, cnpj);
        }
    }
}
