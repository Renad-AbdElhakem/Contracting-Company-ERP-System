using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class AssignProjectEmployeeDto
    {
        public int EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
