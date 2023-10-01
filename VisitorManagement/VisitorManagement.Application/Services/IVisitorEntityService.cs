using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Application.Services
{
    public interface IVisitorEntityService
    {
        //Task<IEnumerable<HostVisitorRequest>> GetVisitorByVisitorNameAndVisitorContactNumberfromHostRequest(string visitorName, string visitorContact);
        Task<IEnumerable<VisitorEntityDTO>> GetVisitorsByVisitorNameAndContactAsync(string visitorName, string visitorContact);
        Task<VisitorEntityDTO> CreateVisitorAsync(VisitorEntityDTO visitorEntityDTO);
        Task<IEnumerable<VisitorEntityDTO>> GetAllVisitorsAsync();
        //  Task<VisitorEntityDTO> GetVisitorByIdAsync(int visitorEntityId);
        Task<bool> UpdateVisitorAsync(string visitorName, string visitorContactNumber, VisitorEntity updatedVisitor);
        Task<bool> DeleteVisitorAsync(string visitorName, string visitorContactNumber);
    }
}
