using AutoMapper;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;
using VisitorManagement.Domain.Exceptions;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application.Services
{
    public class VisitorEntityService : IVisitorEntityService
    {

        private readonly IVisitorEntityRepository _repository;
        private readonly IMapper _mapper;

        public VisitorEntityService(IVisitorEntityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VisitorEntityDTO>> GetVisitorsByVisitorNameAndContactAsync(string visitorName, string visitorContact)
        {
            var visitors = await _repository.GetVisitorByVisitorNameAndVisitorContactNumberfromVisitorEntity(visitorName, visitorContact);

            if (visitors == null || !visitors.Any())
            {
                throw new VisitorEntityNotFoundException($"Visitors with name {visitorName} and contact number {visitorContact} not found.");
            }

            return _mapper.Map<IEnumerable<VisitorEntityDTO>>(visitors);
        }

        public async Task<IEnumerable<VisitorEntityDTO>> GetAllVisitorsAsync()
        {
            var visitors = await _repository.GetAllVisitorsAsync();

            if (visitors == null || !visitors.Any())
            {
                throw new VisitorEntityNotFoundException("No visitors found.");
            }

            return _mapper.Map<IEnumerable<VisitorEntityDTO>>(visitors);
        }

        public async Task<VisitorEntityDTO> CreateVisitorAsync(VisitorEntityDTO visitorEntityDTO)
        {
            var existingVisitor = await _repository.GetVisitorByVisitorNameAndVisitorContactNumberfromVisitorEntity(visitorEntityDTO.Name, visitorEntityDTO.ContactNumber);

            if (existingVisitor.Any())
            {
                return _mapper.Map<VisitorEntityDTO>(existingVisitor.First());
            }

            var newVisitor = new VisitorEntity()
            {
                VisitorName = visitorEntityDTO.Name,
                VisitorContactNumber = visitorEntityDTO.ContactNumber,
                VisitorPersonalIdType = visitorEntityDTO.PersonalIdType,
                VisitorPersonalIdNumber = visitorEntityDTO.PersonalIdNumber,
                VisitorPersonalIdCardImage = visitorEntityDTO.PersonalIdCardImage,
                VisitorPersonalImage = visitorEntityDTO.PersonalImage
            };
            var createdVisitor = await _repository.CreateVisitorAsync(newVisitor);
            return _mapper.Map<VisitorEntityDTO>(createdVisitor);
        }

        public async Task<bool> UpdateVisitorAsync(string visitorName, string visitorContactNumber, VisitorEntity updatedVisitor)
        {
            var success = await _repository.UpdateVisitorAsync(visitorName, visitorContactNumber, updatedVisitor);

            if (!success)
            {
                throw new VisitorEntityNotFoundException($"Visitor with name {visitorName} and contact number {visitorContactNumber} not found.");
            }

            return true; // Return true to indicate success
        }

        public async Task<bool> DeleteVisitorAsync(string visitorName, string visitorContactNumber)
        {
            var success = await _repository.DeleteVisitorAsync(visitorName, visitorContactNumber);

            if (!success)
            {
                throw new VisitorEntityNotFoundException($"Visitor with name {visitorName} and contact number {visitorContactNumber} not found.");
            }

            return success;
        }
    }
}
