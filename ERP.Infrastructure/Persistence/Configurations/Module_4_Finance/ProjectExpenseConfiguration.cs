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
    public class ProjectExpenseConfiguration : IEntityTypeConfiguration<ProjectExpense>
    {
        public void Configure(EntityTypeBuilder<ProjectExpense> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 2);

            builder.HasOne(x => x.ProjectPhase)
                   .WithMany(pp => pp.ProjectExpenses)
                   .HasForeignKey(x => x.ProjectPhaseId)
                   .OnDelete(DeleteBehavior.Restrict);   
        }
    }
}
