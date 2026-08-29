using ERP.Domain.Enum;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2___Procurement___Inventory;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? ExpectedEndDate { get; set; }
        public DateOnly? ActualEndDate { get; set; }
        public string Location { get; set; } = string.Empty;

        //Navigation 

        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public ContractProject? ContractProject { get; set; } 
        public ICollection<ProjectWarehouse> ProjectWarehouses { get; set; } = new List<ProjectWarehouse>();
        public ICollection<ProjectPhase>? ProjectPhases { get; set; } = new List<ProjectPhase>();
        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; } = new List<ProjectEmployee>();
        public ICollection<ProjectOrderRequest>? ProjectOrderRequests { get; set; } = new List<ProjectOrderRequest>();
        public ICollection<ProjectEquipment>? ProjectEquipment { get; set; } = new List<ProjectEquipment>();
        public ICollection<ProjectFinancialSnapshot>?  FinancialSnapshots { get; set; } = new List<ProjectFinancialSnapshot>();


    }
}
