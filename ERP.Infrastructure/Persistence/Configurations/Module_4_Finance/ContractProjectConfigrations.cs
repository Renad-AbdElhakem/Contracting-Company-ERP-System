using ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_4_Finance
{
    public class ContractProjectConfigrations : IEntityTypeConfiguration<ContractProject>
    {
        public void Configure(EntityTypeBuilder<ContractProject> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Client)
                   .WithMany(c => c.ContractProjects)
                   .HasForeignKey(x => x.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ClientId);

            builder.Property(x => x.TotalAmount).HasPrecision(18, 2);
            builder.Property(x => x.PenaltyPerDay).HasPrecision(18, 2);
            builder.Property(x => x.MaxPenaltyAmount).HasPrecision(18, 2);
         

               builder.HasOne(x => x.Project)
               .WithOne(p => p.ContractProject)
               .HasForeignKey<ContractProject>(x => x.ProjectId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ProjectId).IsUnique();


            builder.HasMany(x => x.ContractPaymentPlans)
                   .WithOne(ip => ip.ContractProject)
                   .HasForeignKey(ip => ip.ContractId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ContractPaymentRecords)
                   .WithOne(pr => pr.ContractProject)
                   .HasForeignKey(pr => pr.ContractId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
