using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Exceptions
{
    public class AdminApprovalStatusNotFoundException : ApplicationException
    {
        public AdminApprovalStatusNotFoundException(string message) : base(message)
        {
        }

        public AdminApprovalStatusNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
