using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public EmployeeStatus Status { get; set; }
       
        public DateOnly? TerminationDate { get; set; }

        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
    }
}
