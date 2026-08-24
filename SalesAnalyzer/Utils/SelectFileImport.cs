namespace CSVSalesPro.Utils
{
    public class SelectFileImport
    {
        public static string SelectFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                return dialog.FileName;
            }

            return "";
        }
    }
}
