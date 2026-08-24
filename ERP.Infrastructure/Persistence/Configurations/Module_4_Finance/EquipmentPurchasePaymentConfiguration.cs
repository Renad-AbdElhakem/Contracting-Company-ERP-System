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
    public class EquipmentPurchasePaymentConfiguration : IEntityTypeConfiguration<EquipmentPurchasePayment>
    {
        public void Configure(EntityTypeBuilder<EquipmentPurchasePayment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AmountDue).HasPrecision(18, 2);
            builder.Property(x => x.AmountPaid).HasPrecision(18, 2);
            builder.HasIndex(x=>x.DueDate);
        }
    }
}
