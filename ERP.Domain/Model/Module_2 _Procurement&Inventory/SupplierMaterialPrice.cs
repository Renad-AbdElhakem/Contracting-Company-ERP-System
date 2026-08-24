using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class SupplierMaterialPrice
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        
        //Navigation
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
