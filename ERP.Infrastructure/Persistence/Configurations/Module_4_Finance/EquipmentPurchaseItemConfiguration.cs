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
    public class EquipmentPurchaseItemConfiguration : IEntityTypeConfiguration<EquipmentPurchaseItem>
    {
        public void Configure(EntityTypeBuilder<EquipmentPurchaseItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UnitPrice).HasPrecision(18, 2);

            builder.HasOne(x => x.Equipment)
                   .WithMany(e => e.EquipmentPurchaseItem)
                   .HasForeignKey(x => x.EquipmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
