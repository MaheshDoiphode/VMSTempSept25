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
    public class HostVisitorRequestRepository : IHostVisitorRequestRepository
    {
        private readonly VisitorManagementApplicationContext _context;

        public HostVisitorRequestRepository(VisitorManagementApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HostVisitorRequest>> GetAllRequestsAsync()
        {
            return await _context.HostVisitorRequests.ToListAsync();
        }

        public async Task<HostVisitorRequest> GetRequestByIdAsync(int requestId)
        {
            return await _context.HostVisitorRequests.FindAsync(requestId);
        }

        public async Task<HostVisitorRequest> CreateRequestAsync(HostVisitorRequest request)
        {
            _context.HostVisitorRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }


        public async Task<bool> UpdateRequestAsync(HostVisitorRequest request)
        {
            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRequestAsync(HostVisitorRequest request)
        {
            _context.HostVisitorRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
