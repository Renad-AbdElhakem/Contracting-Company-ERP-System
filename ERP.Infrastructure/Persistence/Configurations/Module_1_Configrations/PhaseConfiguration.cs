using ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_1_Configrations
{
    public class PhaseConfiguration : IEntityTypeConfiguration<Phase>
    {
        public void Configure(EntityTypeBuilder<Phase> builder)
        {
            builder.HasMany(ph => ph.ProjectPhases)
                   .WithOne(Pph => Pph.Phase)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(Pph => Pph.PhaseId);
        }
    }
}
