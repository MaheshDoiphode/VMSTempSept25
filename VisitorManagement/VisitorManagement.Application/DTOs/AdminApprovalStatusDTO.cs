using static VisitorManagement.Domain.Common.AdminApprovalStatus;

namespace VisitorManagement.Application.DTOs
{
    public class AdminApprovalStatusDTO
    {
        public int RequestId { get; set; }
        public string Status { get; set; }
        public string AdminFeedback { get; set; }
    }
}