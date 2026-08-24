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
    public class ProjectWarehouseStockConfiguration : IEntityTypeConfiguration<ProjectWarehouseStock>
    {
        public void Configure(EntityTypeBuilder<ProjectWarehouseStock> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Employee)
                   .WithMany(e => e.ProjectWarehouseStocks)
                   .HasForeignKey(x => x.ReceivedByEmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(pws => pws.ProjectWarehouse)
                   .WithMany(pw => pw.ProjectWarehouseStocks)
                   .HasForeignKey(pw => pw.ProjectWarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);



            builder.HasMany(x => x.MaterialConsumptions)
                   .WithOne(mc => mc.ProjectWarehouseStock)
                   .HasForeignKey(mc => mc.ProjectWarehouseStockId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
