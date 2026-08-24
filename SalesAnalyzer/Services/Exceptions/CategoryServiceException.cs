using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Exceptions
{
    internal class CategoryServiceException : ApplicationException
    {
        public CategoryServiceException(string message) : base(message)
        {

        }
    }
}
