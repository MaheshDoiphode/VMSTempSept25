using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class VisitorEntityNotFoundException : ApplicationException
    {
        public VisitorEntityNotFoundException(string message) : base(message)
        {
        }

        public VisitorEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
