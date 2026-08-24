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
    public class CompanyWarehouseConfiguration : IEntityTypeConfiguration<CompanyWarehouse>
    {
        public void Configure(EntityTypeBuilder<CompanyWarehouse> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Equipment)
                   .WithOne(e => e.CompanyWarehouse)
                   .HasForeignKey(e => e.CompanyWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CompanyWarehouseStocks)
                   .WithOne(s => s.CompanyWarehouse)
                   .HasForeignKey(s => s.CompanyWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(w => w.Equipment)
            .WithOne(e => e.CompanyWarehouse)
            .HasForeignKey(e => e.CompanyWarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
