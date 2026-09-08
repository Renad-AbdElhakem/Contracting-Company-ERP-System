using ERP.Domain.Model.Module_2__Procurement_Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_2_Procurement_Inventory
{
    public class CompanyWarehouseStockConfiguration : IEntityTypeConfiguration<CompanyWarehouseStock>
    {
        public void Configure(EntityTypeBuilder<CompanyWarehouseStock> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.CompanyWarehouseStocks)
                   .HasForeignKey(x => x.ReceivedByEmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(s => s.MaterialPurchaseItem)
                  .WithMany(i => i.CompanyWarehouseStocks)
                  .HasForeignKey(s => s.MaterialPurchaseItemId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.ProjectWarehouseStocks)
                .WithOne(p => p.SourceCompanyWarehouseStock)
                .HasForeignKey(p => p.SourceCompanyWarehouseStockId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
