using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Domain.Common
{
    public class AdminApprovalStatus
    {
        [Key]
        public int HostVisitorRequestId { get; set; } // host requesting for visitor 
        public ApprovalStatus Status { get; set; }
        public string AdminFeedback { get; set; }

        [ForeignKey("HostVisitorRequestId")]
        public virtual HostVisitorRequest HostVisitorRequest { get; set; }

        public enum ApprovalStatus
        {
            NotMarked,
            Approved,
            Denied,
            VisitCompleted
        }

    }
}
