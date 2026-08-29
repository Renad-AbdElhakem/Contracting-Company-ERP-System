using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class AssignProjectPhaseDto
    {
        public Guid ProjectId { get; set; }
        public int PhaseId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly DueDate { get; set; }
        public decimal EstimatedCost { get; set; }
    }
}
