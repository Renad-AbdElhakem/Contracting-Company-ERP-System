using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos
{
    public class OrderMaterialDto
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialName { get; set; }
    }
}
