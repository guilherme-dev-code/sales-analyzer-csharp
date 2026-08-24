using CSVSalesPro.Entities;

namespace CSVSalesPro.Utils
{
    public class SearchBranch
    {
        public static Branch? SearchBranchByName()
        {

            Console.Write("Enter with the branch name: ");
            string nameBranch = (Console.ReadLine() ?? "").ToUpper().Trim();
            Branch? branch = ApplicationContext.Enterprise!.SearchNameBranch(nameBranch);

            return branch;
        }
    }
}
