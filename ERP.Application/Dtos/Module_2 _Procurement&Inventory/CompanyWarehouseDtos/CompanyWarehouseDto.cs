using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.CompanyWarehouseDtos
{
    public class CompanyWarehouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal? CapacitySquareMeters { get; set; }
        public bool IsFull { get; set; }
    }
}
