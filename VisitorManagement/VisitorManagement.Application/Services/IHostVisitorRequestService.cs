using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Application.Services
{
    public interface IHostVisitorRequestService
    {
        Task<IEnumerable<HostVisitorRequestDTO>> GetAllRequestsAsync();
        Task<HostVisitorRequestDTO> GetRequestByIdAsync(int requestId);
        Task<HostVisitorRequestDTO> CreateRequestAsync(HostVisitorRequestDTO request);
        Task<bool> UpdateRequestAsync(int requestId, HostVisitorRequestDTO requestDTO);
        Task<bool> DeleteRequestAsync(int requestId);

    }
}