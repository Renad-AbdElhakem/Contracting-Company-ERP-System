using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class ProjectWithPhasesDto
    {
        public Guid ProjectId { get; set; }

        public List<ProjectPhasesDetailsDto> ProjectPhasesDetailsDtos = new List<ProjectPhasesDetailsDto>();
    }
}
