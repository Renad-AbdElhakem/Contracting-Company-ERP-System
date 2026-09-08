using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos
{
    public class ProjectWarehouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal? CapacitySquareMeters { get; set; }
        public Guid ProjectId { get; set; }
        
    }
}
