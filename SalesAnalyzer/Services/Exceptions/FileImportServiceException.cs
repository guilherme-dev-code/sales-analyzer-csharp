using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Exceptions
{
    internal class FileImportServiceException : ApplicationException
    {
        public FileImportServiceException(string message) : base(message)
        {

        }
    }
}
