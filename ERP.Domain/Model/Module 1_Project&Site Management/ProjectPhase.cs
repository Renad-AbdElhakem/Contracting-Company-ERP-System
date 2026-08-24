using ERP.Domain.Model.Module_2___Procurement___Inventory;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model._1_Project_Site_Management
{
    public class ProjectPhase
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? FinishedDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime? ActualCostCalculatedAt { get; set; }

        //Navigation
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public int PhaseId { get; set; }
        public Phase Phase { get; set; }

        public ICollection<PhaseMaterialRequirement> ProjectMaterials { get; set; } = new List<PhaseMaterialRequirement>();
        public ICollection<MaterialConsumption>? MaterialConsumptions { get; set; } = new List<MaterialConsumption>();
        public ICollection<ProjectExpense>? ProjectExpenses { get; set; } = new List<ProjectExpense>();
    }
}
