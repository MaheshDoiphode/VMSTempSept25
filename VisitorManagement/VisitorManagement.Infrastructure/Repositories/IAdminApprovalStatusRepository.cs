using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Infrastructure.Repositories
{
    public interface IAdminApprovalStatusRepository
    {
        Task<HostVisitorRequest> GetHostVisitorRequestByIdAsync(int requestId);
        Task<IEnumerable<AdminApprovalStatus>> GetAllAdminApprovalStatusesAsync();
        Task<AdminApprovalStatus> GetAdminApprovalStatusAsync(int hostVisitorRequestId);
        Task<bool> CreateAdminApprovalStatusAsync(AdminApprovalStatus adminApprovalStatus);
        Task<bool> UpdateAdminApprovalStatusToApprovedAsync(int requestId);
        Task<bool> UpdateAdminApprovalStatusToDeniedAsync(int requestId, string denialReason);
        Task<bool> UpdateAdminApprovalStatusToVisitCompleted(int requestId);

        //

        Task<IEnumerable<AdminApprovalStatus>> GetAllPendingNotApprovedVisits();
        Task<IEnumerable<AdminApprovalStatus>> GetNotApprovedTomorrowVisitsAsync();
        Task<IEnumerable<AdminApprovalStatus>> GetAllDeniedVisitsAsync();
        Task<IEnumerable<AdminApprovalStatus>> GetAllApprovedVisitsAsync();
        Task<IEnumerable<AdminApprovalStatus>> GetAllCompletedVisitsAsync();
    
    }
}