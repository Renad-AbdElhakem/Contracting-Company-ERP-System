using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos
{
    public class CreateMaterialPurchaseItemDto
    {
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Guid MaterialId { get; set; }
 
    }
}
