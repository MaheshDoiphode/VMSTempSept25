using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitorManagement.Domain.Common
{
    public class HostVisitorRequest
    {
        [Key]
        public int HostVisitorRequestId { get; set; } // host requesting for visitor 
        public string VisitorName { get; set; }
        public string VisitorEmail { get; set; }
        public string VisitorContactNumber { get; set; }
        public DateTime VisitorArrivalDateTime { get; set; }
        public DateTime VisitorCheckOutDateTime { get; set; }
        public string VisitorVisitPurpose { get; set; }
        public TimeSpan VisitDuration { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }

        // Define a navigation property to AdminApprovalStatus
        public virtual ICollection<AdminApprovalStatus> AdminApprovalStatuses { get; set; }
    }
}