using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model._1_Project_Site_Management
{
    public class ProjectEmployee
    {
        public Guid Id { get; set; }

        //Navigation
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
