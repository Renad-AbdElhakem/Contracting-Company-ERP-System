using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.CompanyWarehouseDtos
{
    public class AddCompanyWarehouseStockDto
    {
        public Guid MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public int ReceivedByEmployeeId { get; set; }
    }
}
