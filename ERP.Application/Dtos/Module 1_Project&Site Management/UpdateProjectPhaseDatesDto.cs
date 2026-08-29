using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class UpdateProjectPhaseDatesDto
    {
        public int ProjectPhaseId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? DueDate { get; set; }
    }
}
