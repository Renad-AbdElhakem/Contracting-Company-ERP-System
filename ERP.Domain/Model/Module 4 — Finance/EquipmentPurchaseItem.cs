using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class EquipmentPurchaseItem
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid EquipmentPurchaseId { get; set; } 
        public EquipmentPurchase EquipmentPurchase { get; set; }
        public Guid EquipmentId { get; set; }     
        public Equipment Equipment { get; set; }
    }
}
