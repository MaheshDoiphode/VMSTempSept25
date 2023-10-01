using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Infrastructure.Repositories
{
    public interface IVisitorEntityRepository
    {
        Task<IEnumerable<VisitorEntity>> GetVisitorByVisitorNameAndVisitorContactNumberfromVisitorEntity(string visitorName, string visitorContact);
        Task<IEnumerable<VisitorEntity>> GetAllVisitorsAsync();
        Task<VisitorEntity> CreateVisitorAsync(VisitorEntity visitorEntity);
        Task<bool> UpdateVisitorAsync(string visitorName, string visitorContactNumber, VisitorEntity updatedVisitor);
        Task<bool> DeleteVisitorAsync(string visitorName, string visitorContactNumber);
    }
}
