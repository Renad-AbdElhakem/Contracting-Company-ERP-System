using ERP.Domain.Model.Module_4___Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_4_Finance
{
    public class ProjectFinancialSnapshotConfiguration : IEntityTypeConfiguration<ProjectFinancialSnapshot>
    {
        public void Configure(EntityTypeBuilder<ProjectFinancialSnapshot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ActualRevenue).HasPrecision(18, 2);
            builder.Property(x => x.ActualCost).HasPrecision(18, 2);
            builder.Property(x => x.ActualProfitLoss).HasPrecision(18, 2);

            builder.HasOne(x => x.Project)
                   .WithMany(p => p.FinancialSnapshots)
                   .HasForeignKey(x => x.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);   // real historical financial record, shouldn't vanish

            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.ProjectFinancialSnapshots)
                   .HasForeignKey(x => x.CreatedByEmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);   // same accountability pattern as ReceivedByEmployeeId
        }
    }
}
