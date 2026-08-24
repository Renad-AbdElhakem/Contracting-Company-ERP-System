using ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence.Configurations.Module_1_Configrations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Role)
                   .WithMany(r => r.Employees)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(x => x.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        
            builder.HasMany(x => x.MaintenanceEmployees)
                   .WithOne(me => me.Employee)
                   .HasForeignKey(me => me.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
