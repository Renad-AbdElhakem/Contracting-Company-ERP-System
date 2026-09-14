using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_2__Procurement_Inventory
{
    public class CompanyWarehouseStock
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public ReceivingStatus ReceivingStatus { get; set; }

        //Navigation
        public int CompanyWarehouseId { get; set; }
        public CompanyWarehouse CompanyWarehouse { get; set; }
        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
        public int ReceivedByEmployeeId { get; set; }  
        public Employee Employee { get; set; }
        public Guid ?MaterialPurchaseItemId { get; set; }
        public MaterialPurchaseItem? MaterialPurchaseItem { get; set; }

    
        public ICollection<StockTransfer>?  StockTransfers { get; set; } = new List<StockTransfer>();
    }
}
