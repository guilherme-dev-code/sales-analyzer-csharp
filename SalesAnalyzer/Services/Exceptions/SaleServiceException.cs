using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Exceptions
{
    internal class SaleServiceException : ApplicationException
    {
        public SaleServiceException(string message) : base(message)
        {

        }
    }
}
