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
    public class Equipment_MaintenanceConfiguration : IEntityTypeConfiguration<Equipment_Maintenance>
    {
        public void Configure(EntityTypeBuilder<Equipment_Maintenance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.MaintenanceEmployees)
                   .WithOne(me => me.Equipment_Maintenance)
                   .HasForeignKey(me => me.Equipment_MaintenanceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
