using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_2__Procurement_Inventory
{
    public class OrderMaterials
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }

        //Navigation
        public Guid OrderId { get; set; }
        public ProjectOrderRequest Order { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
