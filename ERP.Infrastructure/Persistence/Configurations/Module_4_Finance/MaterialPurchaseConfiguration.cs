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
    public class MaterialPurchaseConfiguration : IEntityTypeConfiguration<MaterialPurchase>
    {
        public void Configure(EntityTypeBuilder<MaterialPurchase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalAmount).HasPrecision(18, 2);

            builder.HasOne(x => x.Supplier)
                   .WithMany(s => s.MaterialPurchases)
                   .HasForeignKey(x => x.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);
            

            builder.HasMany(x => x.MaterialPurchaseItems)
                   .WithOne(i => i.MaterialPurchase)
                   .HasForeignKey(i => i.MaterialPurchaseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Payments)
                   .WithOne(p => p.MaterialPurchase)
                   .HasForeignKey(p => p.MaterialPurchaseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
