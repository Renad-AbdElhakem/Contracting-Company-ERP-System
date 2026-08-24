using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
   public class MaterialPurchaseItem
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        //Navigation
        public Guid MaterialPurchaseId { get; set; }
        public MaterialPurchase MaterialPurchase { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material {  get; set; }
      
    }
}
