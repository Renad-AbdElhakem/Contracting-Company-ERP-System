using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class MaterialPurchase
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateOnly ArrivalDate { get; set; }
        //Navigation
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<MaterialPurchaseItem>? MaterialPurchaseItems { get; set; } = new List<MaterialPurchaseItem>();
        public ICollection<MaterialPurchasePayment> Payments { get; set; } = new List<MaterialPurchasePayment>();
    }
}
