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
    public class VisitorEntityRepository : IVisitorEntityRepository
    {


        private readonly VisitorManagementApplicationContext _context;

        public VisitorEntityRepository(VisitorManagementApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VisitorEntity>> GetVisitorByVisitorNameAndVisitorContactNumberfromVisitorEntity(string visitorName, string visitorContact)
        {
            return await _context.Visitors
                .Where(visitor => visitor.VisitorName == visitorName && visitor.VisitorContactNumber == visitorContact)
                .ToListAsync();
        }

        public async Task<IEnumerable<VisitorEntity>> GetAllVisitorsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task<VisitorEntity> CreateVisitorAsync(VisitorEntity visitorEntity)
        {
            _context.Visitors.Add(visitorEntity);
            await _context.SaveChangesAsync();
            return visitorEntity;
        }

        public async Task<bool> UpdateVisitorAsync(string visitorName, string visitorContactNumber, VisitorEntity updatedVisitor)
        {
            var visitorEntity = await _context.Visitors
                .SingleOrDefaultAsync(v => v.VisitorName == visitorName && v.VisitorContactNumber == visitorContactNumber);

            if (visitorEntity == null)
            {
                return false;
            }

            visitorEntity.VisitorName = updatedVisitor.VisitorName;
            visitorEntity.VisitorContactNumber = updatedVisitor.VisitorContactNumber;
            visitorEntity.VisitorPersonalIdType = updatedVisitor.VisitorPersonalIdType;
            visitorEntity.VisitorPersonalIdNumber = updatedVisitor.VisitorPersonalIdNumber;
            visitorEntity.VisitorPersonalIdCardImage = updatedVisitor.VisitorPersonalIdCardImage;
            visitorEntity.VisitorPersonalImage = updatedVisitor.VisitorPersonalImage;

            await _context.SaveChangesAsync(); // Save the changes to the database
            return true; // Return true to indicate success
        }


        public async Task<bool> DeleteVisitorAsync(string visitorName, string visitorContactNumber)
        {
            var visitorEntity = await _context.Visitors
            .SingleOrDefaultAsync(v => v.VisitorName == visitorName && v.VisitorContactNumber == visitorContactNumber);
            if (visitorEntity == null)
            {
                return false;
            }

            _context.Visitors.Remove(visitorEntity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
