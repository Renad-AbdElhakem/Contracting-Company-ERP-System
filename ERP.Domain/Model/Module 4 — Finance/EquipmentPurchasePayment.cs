using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class EquipmentPurchasePayment
    {
        public Guid Id { get; set; }
        public decimal AmountDue { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? PaidDate { get; set; }
        
        //Navigation
        public Guid EquipmentPurchaseId { get; set; }
        public EquipmentPurchase EquipmentPurchase { get; set; }
    }
}
