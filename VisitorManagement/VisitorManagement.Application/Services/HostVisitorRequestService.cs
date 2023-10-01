using AutoMapper;
using NuGet.Protocol.Core.Types;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;
using VisitorManagement.Domain.Exceptions;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application.Services
{
    public class HostVisitorRequestService : IHostVisitorRequestService
    {
        private readonly IHostVisitorRequestRepository _repository;
        private readonly IMapper _mapper;

        public HostVisitorRequestService(IHostVisitorRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HostVisitorRequestDTO>> GetAllRequestsAsync()
        {
            var requests = await _repository.GetAllRequestsAsync();
            return _mapper.Map<IEnumerable<HostVisitorRequestDTO>>(requests);
        }

        public async Task<HostVisitorRequestDTO> GetRequestByIdAsync(int requestId)
        {
            var request = await _repository.GetRequestByIdAsync(requestId);

            if (request == null)
            {
                throw new HostVisitorRequestNotFoundException($"Host Visitor Request with ID {requestId} not found.");
            }

            return _mapper.Map<HostVisitorRequestDTO>(request);
        }

        public async Task<HostVisitorRequestDTO> CreateRequestAsync(HostVisitorRequestDTO requestDTO)
        {
            var request = _mapper.Map<HostVisitorRequest>(requestDTO);
            request.VisitDuration = requestDTO.CheckOutDateTime - requestDTO.ArrivalDateTime;
            var createdRequest = await _repository.CreateRequestAsync(request);
            requestDTO.Duration = createdRequest.VisitDuration;
            requestDTO.RequestId = createdRequest.HostVisitorRequestId;

            return _mapper.Map<HostVisitorRequestDTO>(request);
        }

        public async Task<bool> UpdateRequestAsync(int requestId, HostVisitorRequestDTO requestDTO)
        {
            var existingRequest = await _repository.GetRequestByIdAsync(requestId);

            if (existingRequest == null)
            {
                throw new HostVisitorRequestNotFoundException($"Host Visitor Request with ID {requestId} not found.");
            }

            _mapper.Map(requestDTO, existingRequest);

            if (!await _repository.UpdateRequestAsync(existingRequest))
            {
                throw new HostVisitorRequestServiceException($"Failed to update Host Visitor Request with ID {requestId}.");
            }

            return true;
        }

        public async Task<bool> DeleteRequestAsync(int requestId)
        {
            var existingRequest = await _repository.GetRequestByIdAsync(requestId);

            if (existingRequest == null)
            {
                throw new HostVisitorRequestNotFoundException($"Host Visitor Request with ID {requestId} not found.");
            }

            if (!await _repository.DeleteRequestAsync(existingRequest))
            {
                throw new HostVisitorRequestServiceException($"Failed to delete Host Visitor Request with ID {requestId}.");
            }

            return true;
        }

        



    }
}
