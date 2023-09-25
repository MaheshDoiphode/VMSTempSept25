using VisitorManagement.Application.DTOs;

namespace VisitorManagement.Application.Services
{
    public interface IAdminApprovalStatusService
    {
        Task<HostVisitorRequestDTO> GetHostVisitorRequestByIdAsync(int requestId);
        Task<AdminApprovalStatusDTO> GetAdminApprovalStatusAsync(int hostVisitorRequestId);
        Task<IEnumerable<AdminApprovalStatusDTO>> GetAllAdminApprovalStatusesAsync();
        Task<bool> UpdateAdminApprovalStatusToApprovedAsync(int requestId);
        Task<bool> UpdateAdminApprovalStatusToDeniedAsync(int requestId);
        Task<bool> UpdateAdminApprovalStatusToVisitCompleted(int requestId);

        //

        Task<IEnumerable<AdminApprovalStatusDTO>> GetAllPendingNotApprovedVisits();
        Task<IEnumerable<AdminApprovalStatusDTO>> GetNotApprovedTomorrowVisitsAsync();
        Task<IEnumerable<AdminApprovalStatusDTO>> GetAllDeniedVisitsAsync();
        Task<IEnumerable<AdminApprovalStatusDTO>> GetAllApprovedVisitsAsync();
        Task<IEnumerable<AdminApprovalStatusDTO>> GetAllCompletedVisitsAsync();

    }
}