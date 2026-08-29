using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class CreateProjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? ExpectedEndDate { get; set; }
        public string Location { get; set; } = string.Empty;

        public Guid ClientId { get; set; }
    }
}
