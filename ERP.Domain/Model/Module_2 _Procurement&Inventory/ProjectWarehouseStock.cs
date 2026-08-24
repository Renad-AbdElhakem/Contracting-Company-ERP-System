using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_2__Procurement_Inventory
{
    public class ProjectWarehouseStock
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }
        public DateTime ArrivalDate { get; set; }

        //Navigation

        public int ProjectWarehouseId { get; set; }
        public ProjectWarehouse ProjectWarehouse { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }

        public int ReceivedByEmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<MaterialConsumption>? MaterialConsumptions { get; set; }=new List<MaterialConsumption>();
    }
}
