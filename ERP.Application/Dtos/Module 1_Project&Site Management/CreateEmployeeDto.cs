using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } 
        public string Address { get; set; } 
        public decimal Salary { get; set; }
        public EmployeeStatus Status { get; set; }
        public DateOnly HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }

        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
    }
}
