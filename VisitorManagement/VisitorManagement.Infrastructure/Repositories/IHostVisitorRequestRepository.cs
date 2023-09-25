using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Infrastructure.Repositories
{
    public interface IHostVisitorRequestRepository
    {
        Task<IEnumerable<HostVisitorRequest>> GetAllRequestsAsync();
        Task<HostVisitorRequest> GetRequestByIdAsync(int requestId);
        Task<HostVisitorRequest> CreateRequestAsync(HostVisitorRequest request);
        Task<bool> UpdateRequestAsync(HostVisitorRequest request);
        Task<bool> DeleteRequestAsync(HostVisitorRequest request);

    }
}
