using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Infrastructure.Data
{
    public class VisitorManagementApplicationContext : DbContext
    {
        public VisitorManagementApplicationContext(DbContextOptions<VisitorManagementApplicationContext> options)
    : base(options)
        {
        }

        public DbSet<HostVisitorRequest> HostVisitorRequests { get; set; }

        public DbSet<AdminApprovalStatus> AdminApprovalStatuses { get; set; }
        public DbSet<VisitorEntity> Visitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitorEntity>()
                .HasKey(v => new { v.VisitorName, v.VisitorContactNumber });

            modelBuilder.Entity<VisitorEntity>()
                .Property(v => v.VisitorEntityId)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }


    }
}
