using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class EquipmentPurchase
    {
        public Guid Id { get; set; }  
        public decimal TotalAmount { get; set; }
        public DateOnly ArrivalDate { get; set; }

        //Navigation 
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public ICollection<EquipmentPurchasePayment>?  EquipmentPurchasePayments { get; set; } = new List<EquipmentPurchasePayment>();
        public ICollection<EquipmentPurchaseItem>? EquipmentPurchaseItems { get; set; } = new List<EquipmentPurchaseItem>();
    }
}
