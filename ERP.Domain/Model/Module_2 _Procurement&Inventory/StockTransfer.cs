using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_2__Procurement_Inventory
{
    public class StockTransfer
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public DateTime SentDate { get; set; }
        public StockTransferStatus Status { get; set; }
        public DateTime ?ReceivedDate { get; set; }

        //Navigation 
        public int CompanyWarehouseStockId { get; set; }
        public CompanyWarehouseStock CompanyWarehouseStock { get; set; }

        public int ProjectWarehouseId { get; set; }
        public ProjectWarehouse ProjectWarehouse { get; set; }

        public Guid OrderMaterialId { get; set; }
        public OrderMaterials OrderMaterial { get; set; }

    }
}
