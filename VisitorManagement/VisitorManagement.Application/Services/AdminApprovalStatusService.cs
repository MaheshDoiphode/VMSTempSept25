using AutoMapper;
using NuGet.Protocol.Core.Types;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;
using VisitorManagement.Domain.Exceptions;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application.Services
{
    public class AdminApprovalStatusService : IAdminApprovalStatusService
    {
        private readonly IAdminApprovalStatusRepository _adminApprovalStatusRepository;
        private readonly IHostVisitorRequestRepository _hostVisitorRequestRepository;
        private readonly IMapper _mapper;

        public AdminApprovalStatusService(
            IAdminApprovalStatusRepository adminApprovalStatusRepository,
            IHostVisitorRequestRepository hostVisitorRequestRepository,
            IMapper mapper)
        {
            _adminApprovalStatusRepository = adminApprovalStatusRepository;
            _hostVisitorRequestRepository = hostVisitorRequestRepository;
            _mapper = mapper;
        }

        public async Task<HostVisitorRequestDTO> GetHostVisitorRequestByIdAsync(int requestId)
        {
            var request = await _hostVisitorRequestRepository.GetRequestByIdAsync(requestId);

            if (request == null)
            {
                throw new HostVisitorRequestNotFoundException($"Host Visitor Request with ID {requestId} not found.");
            }

            return _mapper.Map<HostVisitorRequestDTO>(request);
        }

        public async Task<IEnumerable<AdminApprovalStatusDTO>> GetAllAdminApprovalStatusesAsync()
        {
            var adminApprovalStatuses = await _adminApprovalStatusRepository.GetAllAdminApprovalStatusesAsync();

            if (adminApprovalStatuses == null || !adminApprovalStatuses.Any())
            {
                throw new AdminApprovalStatusNotFoundException("No admin approval statuses found.");
            }

            return _mapper.Map<IEnumerable<AdminApprovalStatusDTO>>(adminApprovalStatuses);
        }


        public async Task<AdminApprovalStatusDTO> GetAdminApprovalStatusAsync(int hostVisitorRequestId)
        {
            var adminApprovalStatus = await _adminApprovalStatusRepository.GetAdminApprovalStatusAsync(hostVisitorRequestId);

            if (adminApprovalStatus == null)
            {
                throw new AdminApprovalStatusNotFoundException($"Admin Approval Status for Request with ID {hostVisitorRequestId} not found.");
            }

            return _mapper.Map<AdminApprovalStatusDTO>(adminApprovalStatus);
        }

        
        public async Task<bool> UpdateAdminApprovalStatusToApprovedAsync(int requestId)
        {
            var adminApprovalStatus = await _adminApprovalStatusRepository.GetAdminApprovalStatusAsync(requestId);

            if (adminApprovalStatus == null)
            {
                throw new AdminApprovalStatusNotFoundException($"Admin Approval Status for Request with ID {requestId} not found.");
            }

            await _adminApprovalStatusRepository.UpdateAdminApprovalStatusToApprovedAsync(requestId);

            return true;
        }

        public async Task<bool> UpdateAdminApprovalStatusToDeniedAsync(int requestId)
        {
            var adminApprovalStatus = await _adminApprovalStatusRepository.GetAdminApprovalStatusAsync(requestId);

            if (adminApprovalStatus == null)
            {
                throw new AdminApprovalStatusNotFoundException($"Admin Approval Status for Request with ID {requestId} not found.");
            }

            await _adminApprovalStatusRepository.UpdateAdminApprovalStatusToDeniedAsync(requestId);

            return true;
        }
        public async Task<bool> UpdateAdminApprovalStatusToVisitCompleted(int requestId)
        {
            var adminApprovalStatus = await _adminApprovalStatusRepository.GetAdminApprovalStatusAsync(requestId);

            if (adminApprovalStatus == null)
            {
                throw new AdminApprovalStatusNotFoundException($"Admin Approval Status for Request with ID {requestId} not found.");
            }

            await _adminApprovalStatusRepository.UpdateAdminApprovalStatusToVisitCompleted(requestId);

            return true;
        }

        //

        public async Task<IEnumerable<AdminApprovalStatusDTO>> GetAllPendingNotApprovedVisits()
        {
            var adminApprovalStatuses = await _adminApprovalStatusRepository.GetAllPendingNotApprovedVisits();
            if (adminApprovalStatuses == null || !adminApprovalStatuses.Any())
            {
                throw new AdminApprovalStatusNotFoundException("No pending, not approved visits found till the current date.");
            }
            return _mapper.Map<IEnumerable<AdminApprovalStatusDTO>>(adminApprovalStatuses);
        }

        public async Task<IEnumerable<AdminApprovalStatusDTO>> GetNotApprovedTomorrowVisitsAsync()
        {
            var adminApprovalStatuses = await _adminApprovalStatusRepository.GetNotApprovedTomorrowVisitsAsync();
            if (adminApprovalStatuses == null || !adminApprovalStatuses.Any())
            {
                throw new AdminApprovalStatusNotFoundException("No not approved visits found for tomorrow.");
            }
            return _mapper.Map<IEnumerable<AdminApprovalStatusDTO>>(adminApprovalStatuses);
        }

        public async Task<IEnumerable<AdminApprovalStatusDTO>> GetAllDeniedVisitsAsync()
        {
            var adminApprovalStatuses = await _adminApprovalStatusRepository.GetAllDeniedVisitsAsync();
            if (adminApprovalStatuses == null || !adminApprovalStatuses.Any())
            {
                throw new AdminApprovalStatusNotFoundException("No denied visits found.");
            }
            return _mapper.Map<IEnumerable<AdminApprovalStatusDTO>>(adminApprovalStatuses);
        }

        public async Task<IEnumerable<AdminApprovalStatusDTO>> GetAllApprovedVisitsAsync()
        {
            var adminApprovalStatuses = await _adminApprovalStatusRepository.GetAllApprovedVisitsAsync();
            if (adminApprovalStatuses == null || !adminApprovalStatuses.Any())
            {
                throw new AdminApprovalStatusNotFoundException("No approved visits found.");
            }
            return _mapper.Map<IEnumerable<AdminApprovalStatusDTO>>(adminApprovalStatuses);
        }

        public async Task<IEnumerable<AdminApprovalStatusDTO>> GetAllCompletedVisitsAsync()
        {
            var adminApprovalStatuses = await _adminApprovalStatusRepository.GetAllCompletedVisitsAsync();
            if (adminApprovalStatuses == null || !adminApprovalStatuses.Any())
            {
                throw new AdminApprovalStatusNotFoundException("No completed visits found.");
            }
            return _mapper.Map<IEnumerable<AdminApprovalStatusDTO>>(adminApprovalStatuses);
        }

    }
}