using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Exceptions
{
    internal class ProductServiceException : ApplicationException
    {
        public ProductServiceException(string message) : base(message)
        {

        }
    }
}
