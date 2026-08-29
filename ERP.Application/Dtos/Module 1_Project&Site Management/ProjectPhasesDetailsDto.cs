using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class ProjectPhasesDetailsDto
    {
        public string PhaseName { get; set; }=string.Empty; 
        public DateOnly StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? FinishedDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }
        public DateTime? ActualCostCalculatedAt { get; set; }
    }
}
