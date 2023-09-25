using VisitorManagement.Domain.Common;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application
{
    public class DataSeeder
    {
        private readonly IHostVisitorRequestRepository _hostVisitorRequestRepository;
        private readonly IAdminApprovalStatusRepository _adminApprovalStatusRepository;

        public DataSeeder(
            IHostVisitorRequestRepository hostVisitorRequestRepository,
            IAdminApprovalStatusRepository adminApprovalStatusRepository)
        {
            _hostVisitorRequestRepository = hostVisitorRequestRepository;
            _adminApprovalStatusRepository = adminApprovalStatusRepository;
        }

        public async Task SeedAdminApprovalStatusesAsync()
        {
            var requestIds = (await _hostVisitorRequestRepository.GetAllRequestsAsync()).Select(request => request.HostVisitorRequestId);

            foreach (var requestId in requestIds)
            {
                var existingStatus = await _adminApprovalStatusRepository.GetAdminApprovalStatusAsync(requestId);

                if (existingStatus == null)
                {
                    var newStatus = new AdminApprovalStatus
                    {
                        HostVisitorRequestId = requestId,
                        Status = AdminApprovalStatus.ApprovalStatus.NotMarked // Set the desired status
                    };

                    // Create a new admin approval status record
                    await _adminApprovalStatusRepository.CreateAdminApprovalStatusAsync(newStatus);
                }

            }
        }
    }

}
