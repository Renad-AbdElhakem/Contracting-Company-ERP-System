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
    public class EquipmentPurchaseConfiguration : IEntityTypeConfiguration<EquipmentPurchase>
    {
        public void Configure(EntityTypeBuilder<EquipmentPurchase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalAmount).HasPrecision(18, 2);

            builder.HasOne(x => x.Supplier)
                   .WithMany(s => s.EquipmentPurchases)
                   .HasForeignKey(x => x.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);
            

            builder.HasMany(x => x.EquipmentPurchaseItems)
                   .WithOne(i => i.EquipmentPurchase)
                   .HasForeignKey(i => i.EquipmentPurchaseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.EquipmentPurchasePayments)
                   .WithOne(p => p.EquipmentPurchase)
                   .HasForeignKey(p => p.EquipmentPurchaseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
