using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
   public class ProjectWarehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal? CapacitySquareMeters { get; set; }

        //Navigation 
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<ProjectWarehouseStock>? ProjectWarehouseStocks { get; set; } = new List<ProjectWarehouseStock>();
    }
}
