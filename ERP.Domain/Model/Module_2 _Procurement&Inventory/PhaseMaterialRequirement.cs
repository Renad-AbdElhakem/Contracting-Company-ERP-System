using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_2___Procurement___Inventory
{
    public class PhaseMaterialRequirement
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public string? Notes { get; set; }

        //Navigation

        public int ProjectPhaseId { get; set; }  
        public ProjectPhase ProjectPhase { get; set; }

        public Guid MaterialId { get; set; }
        public Material Material { get; set; }
    }
}
