using static VisitorManagement.Domain.Common.HostVisitorRequest;

namespace VisitorManagement.Application.DTOs
{
    public class HostVisitorRequestDTO
    {
        public int RequestId { get; set; }
        public string VisitorFullName { get; set; }
        public string VisitorEmailAddress { get; set; }
        public string VisitorPhone { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string VisitPurpose { get; set; }
        public int RequestingHostId { get; set; }
        public string RequestingHostName { get; set; }
    }
}