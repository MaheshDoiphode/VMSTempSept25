using static VisitorManagement.Domain.Common.AdminApprovalStatus;

namespace VisitorManagement.Application.DTOs
{
    public class AdminApprovalStatusDTO
    {
        public int HostVisitorRequestId { get; set; }
        public string Status { get; set; }
        public string AdminFeedback { get; set; }
    }
}