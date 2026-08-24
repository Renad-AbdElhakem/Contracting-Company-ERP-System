using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_2__Procurement_Inventory
{
    public class MaterialConsumption
    {
        public int Id { get; set; }
        public decimal QuantityUsed { get; set; }
        public DateOnly Date { get; set; }
        public string? Note { get; set; }

        //Navigation 

        public int ProjectWarehouseStockId { get; set; }   // FK → ProjectWarehouseStock (tells you the material)
        public ProjectWarehouseStock ProjectWarehouseStock { get; set; }

        public int ProjectPhaseId { get; set; }             // FK → ProjectPhase (which phase used it)
        public ProjectPhase ProjectPhase { get; set; }
     
    }
}
