using ERP.Domain.Enum;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_1_Project_Site_Management;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public EmployeeStatus Status { get; set; }
        public DateOnly HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }


        //Navigation 

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; } = new List<ProjectEmployee>();
        public ICollection<ProjectWarehouseStock>? ProjectWarehouseStocks { get; set; } = new List<ProjectWarehouseStock>();
        public ICollection<CompanyWarehouseStock>? CompanyWarehouseStocks { get; set; } = new List<CompanyWarehouseStock>();
        public ICollection<ProjectOrderRequest>? ProjectOrderRequests { get; set; } = new List<ProjectOrderRequest>();
        public ICollection<MaintenanceEmployee>? MaintenanceEmployees { get; set; } = new List<MaintenanceEmployee>();
        public ICollection<ProjectFinancialSnapshot>? ProjectFinancialSnapshots { get; set; } = new List<ProjectFinancialSnapshot>();
    }
}
