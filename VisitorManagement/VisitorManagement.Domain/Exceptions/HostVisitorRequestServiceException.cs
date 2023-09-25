using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class HostVisitorRequestServiceException : ApplicationException
    {
        public HostVisitorRequestServiceException(string message) : base(message)
        {
        }

        public HostVisitorRequestServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}