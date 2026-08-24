using ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_3_Equipment_Machinery
{
    public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Equipment_Maintenances)
                   .WithOne(m => m.Equipment)
                   .HasForeignKey(m => m.EquipmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProjectEquipment)
                   .WithOne(pe => pe.Equipment)
                   .HasForeignKey(pe => pe.EquipmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.EquipmentPurchaseItem)
                   .WithOne(epi => epi.Equipment)
                   .HasForeignKey(epi => epi.EquipmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
