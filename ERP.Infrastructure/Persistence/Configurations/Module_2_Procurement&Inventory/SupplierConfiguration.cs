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
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.SupplierMaterials)
                   .WithOne(smp => smp.Supplier)
                   .HasForeignKey(smp => smp.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Equipments)
                   .WithOne(e => e.Supplier)
                   .HasForeignKey(e => e.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
