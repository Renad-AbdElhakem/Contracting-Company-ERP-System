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
    public class MaterialPurchaseItemConfiguration : IEntityTypeConfiguration<MaterialPurchaseItem>
    {
        public void Configure(EntityTypeBuilder<MaterialPurchaseItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UnitPrice).HasPrecision(18, 2);

            builder.HasOne(x => x.Material)
                   .WithMany(m => m.MaterialPurchaseItem)
                   .HasForeignKey(x => x.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
