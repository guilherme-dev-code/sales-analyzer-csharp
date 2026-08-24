using System;
using System.Collections.Generic;
using System.Text;

namespace CSVSalesPro.Services.Exceptions
{
    internal class RepositoryException : ApplicationException
    {
        public RepositoryException(string message) : base(message)
        {

        }
    }
}
