using ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_2_Procurement_Inventory
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.SupplierMaterials)
                   .WithOne(smp => smp.Material)
                   .HasForeignKey(smp => smp.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CompanyWarehouseStocks)
                   .WithOne(s => s.Material)
                   .HasForeignKey(s => s.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProjectWarehouseStocks)
                   .WithOne(s => s.Material)
                   .HasForeignKey(s => s.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.OrderMaterials)
                   .WithOne(om => om.Material)
                   .HasForeignKey(om => om.MaterialId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PhaseMaterialRequirements)
                   .WithOne(pmr => pmr.Material)
                   .HasForeignKey(pmr => pmr.MaterialId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
