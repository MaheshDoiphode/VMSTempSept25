using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class VisitorEntityServiceException : ApplicationException
    {
        public VisitorEntityServiceException(string message) : base(message)
        {
        }

        public VisitorEntityServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
