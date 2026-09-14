using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.StockTransfer
{
   public class CreateNewStockTransferDto
    {
        public decimal Quantity { get; set; }
        public int CompanyWarehouseStockId { get; set; }
        public Guid OrderMaterialId { get; set; }
    }
}
