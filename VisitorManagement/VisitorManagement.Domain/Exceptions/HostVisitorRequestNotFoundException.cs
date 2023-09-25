using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class HostVisitorRequestNotFoundException : ApplicationException
    {
        public HostVisitorRequestNotFoundException(string message) : base(message)
        {
        }

        public HostVisitorRequestNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
