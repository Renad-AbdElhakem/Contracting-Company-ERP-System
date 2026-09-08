using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos
{
    public class UpdateProjectWarehouseDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public decimal? CapacitySquareMeters { get; set; }
    }
}
