using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model._1_Project_Site_Management
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        //Navigation 

        public ICollection<Project> Projects { get; set; }= new List<Project>();
        public ICollection<ContractProject> ContractProjects { get; set; }= new List<ContractProject>();
    }
}
