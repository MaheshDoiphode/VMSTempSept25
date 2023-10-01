using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;
using VisitorManagement.Infrastructure.Data;

namespace VisitorManagement.Infrastructure.Repositories
{
    public class AdminApprovalStatusRepository : IAdminApprovalStatusRepository
    {
        private readonly VisitorManagementApplicationContext _context;

        public AdminApprovalStatusRepository(VisitorManagementApplicationContext context)
        {
            _context = context;
        }

        public async Task<HostVisitorRequest> GetHostVisitorRequestByIdAsync(int requestId)
        {
            return await _context.HostVisitorRequests.FindAsync(requestId);
        }
        public async Task<IEnumerable<AdminApprovalStatus>> GetAllAdminApprovalStatusesAsync()
        {
            return await _context.AdminApprovalStatuses.ToListAsync();
        }

        public async Task<AdminApprovalStatus> GetAdminApprovalStatusAsync(int hostVisitorRequestId)
        {
            return await _context.AdminApprovalStatuses.FindAsync(hostVisitorRequestId);
        }

        public async Task<bool> UpdateAdminApprovalStatusToApprovedAsync(int requestId)
        {
            var request = await _context.AdminApprovalStatuses.FindAsync(requestId);
            request.Status = AdminApprovalStatus.ApprovalStatus.Approved;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAdminApprovalStatusToDeniedAsync(int requestId, string denialReason)
        {
            var request = await _context.AdminApprovalStatuses.FindAsync(requestId);
            request.Status = AdminApprovalStatus.ApprovalStatus.Denied;
            request.AdminFeedback = denialReason;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAdminApprovalStatusToVisitCompleted(int requestId)
        {
            var request = await _context.AdminApprovalStatuses.FindAsync(requestId);
            request.Status = AdminApprovalStatus.ApprovalStatus.VisitCompleted;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CreateAdminApprovalStatusAsync(AdminApprovalStatus adminApprovalStatus)
        {
            _context.AdminApprovalStatuses.Add(adminApprovalStatus);
            await _context.SaveChangesAsync();
            return true;
        }

        //

        public async Task<IEnumerable<AdminApprovalStatus>> GetAllPendingNotApprovedVisits()
        {
            var currentDate = DateTime.Now;

            return await _context.AdminApprovalStatuses
                .Where(status => status.Status == AdminApprovalStatus.ApprovalStatus.NotMarked )
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminApprovalStatus>> GetNotApprovedTomorrowVisitsAsync()
        {
            var tomorrow = DateTime.Now.AddDays(1).Date;

            return await _context.AdminApprovalStatuses
                .Where(status => status.Status == AdminApprovalStatus.ApprovalStatus.NotMarked &&
                                 status.HostVisitorRequest.VisitorArrivalDateTime.Date == tomorrow)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminApprovalStatus>> GetAllDeniedVisitsAsync()
        {
            return await _context.AdminApprovalStatuses
                .Where(status => status.Status == AdminApprovalStatus.ApprovalStatus.Denied)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminApprovalStatus>> GetAllApprovedVisitsAsync()
        {
            return await _context.AdminApprovalStatuses
                .Where(status => status.Status == AdminApprovalStatus.ApprovalStatus.Approved)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminApprovalStatus>> GetAllCompletedVisitsAsync()
        {
            return await _context.AdminApprovalStatuses
                .Where(status => status.Status == AdminApprovalStatus.ApprovalStatus.VisitCompleted)
                .ToListAsync();
        }



    }
}
