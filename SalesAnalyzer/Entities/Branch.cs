using CSVSalesPro.Entities.Exceptions;

namespace CSVSalesPro.Entities
{
    public class Branch
    {
        private static int NextId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }

        public Address Address { get; }

        public List<ImportedFile> ImportedFiles { get; set; } = new List<ImportedFile>();

        public Branch() { }

        public Branch(string name, Address address)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BranchException("Name is required!");
            }

            Id = NextId++;
            Name = name;
            Address = address;
        }

        public void AddFile(ImportedFile file)
        {
            bool validating = ValidatingImportedFile(file);

            if (validating)
            {
                throw new BranchException($"The file already exists in the list!");
            }
            else
            {
                ImportedFiles.Add(file);
            }
        }

        public void RemoveFile(ImportedFile file)
        {
            bool validating = ValidatingImportedFile(file);

            if (!validating)
            {
                throw new BranchException("The file actualy cannot exists in the list! Please add the file in list!");
            }
            else
            {
                ImportedFiles.Remove(file);
            }
        }

        public ImportedFile SearchFile(int id)
        {
            return ImportedFiles.Find(x => x.Id == id);
        }

        public override string ToString()
        {
            return $"Branch name: {Name}\n\nAddress: {Address}";
        }

        private bool ValidatingImportedFile(ImportedFile file)
        {
            file = ImportedFiles.Find(x => x.Id == file.Id);

            if(file != null)
            {
                return true;
            }
            return false;
        }
    }
}
