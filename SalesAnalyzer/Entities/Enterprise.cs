using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    public class Enterprise
    {
        private static int NextId = 1;
        public int Id { get; private set; }
        public string SocialReason { get; set; }
        public string CNPJ { get; set; }
        public List<Branch> Branches { get; set; }  = new List<Branch>();

        public Enterprise() { }

        public Enterprise(string socialReason, string cnpj)
        {
            if (string.IsNullOrWhiteSpace(socialReason))
            {
                throw new EnterpriseException("Social Reason is required!");
            }

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                throw new EnterpriseException("CNPJ is required!");
            }

            if(cnpj.Length != 14)
            {
                throw new EnterpriseException("CNPJ must have 14 digits!");
            }

            Id = NextId++;
            SocialReason = socialReason;
            CNPJ = cnpj;
        }

        public void AddBranch(Branch branch)
        {
            bool validating = ValidatingBranch(branch);
            if (validating)
            {
                throw new EnterpriseException($"The branch {branch.Name} already exists in the list");
            }
            else
            {
                Branches.Add(branch);
            }
        }

        public void RemoveBranch(Branch branch)
        {
            bool validating = ValidatingBranch(branch);
            if (!validating)
            {
                throw new EnterpriseException($"The branch {branch.Name} actually cannot exists int the list");
            }
            Branches.Remove(branch);
        }

        public Branch SearchIdBranch(int id)
        {
            return Branches.Find(x => x.Id == id);
        }

        public Branch SearchNameBranch(string name)
        {
            return Branches.Find(x => x.Name == name);
        }

        private bool ValidatingBranch(Branch searchBranch)
        {
            searchBranch = Branches.Find(x => x.Name == searchBranch.Name);

            if(searchBranch != null)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Social Reason: {SocialReason}\nCNPJ: {CNPJ}\nBranches: {Branches.Count}";
        }

    }
}
