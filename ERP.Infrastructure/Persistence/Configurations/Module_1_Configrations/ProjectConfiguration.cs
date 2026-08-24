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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ContractProject)
                   .WithOne(c => c.Project)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Client)
                   .WithMany(c => c.Projects)
                   .HasForeignKey(c => c.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProjectPhases)
                   .WithOne(p => p.Project)
                   .HasForeignKey(p => p.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProjectWarehouses)
                   .WithOne(w => w.Project)
                   .HasForeignKey(w => w.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProjectOrderRequests)
                   .WithOne(o => o.Project)
                   .HasForeignKey(o => o.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProjectEquipment)
                   .WithOne(pe => pe.Project)
                   .HasForeignKey(pe => pe.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProjectEmployees)
                   .WithOne(pe => pe.Project)
                   .HasForeignKey(pe => pe.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
