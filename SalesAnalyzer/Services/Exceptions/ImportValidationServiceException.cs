using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Exceptions
{
    internal class ImportValidationServiceException : ApplicationException
    {
        public ImportValidationServiceException(string message) : base(message)
        {

        }
    }
}
