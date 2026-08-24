using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_3___Equipment_Machinery
{
    public class MaintenanceEmployee
    {
        public int Id { get; set; }

        //Navigation 

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid Equipment_MaintenanceId { get; set; }
        public Equipment_Maintenance Equipment_Maintenance { get; set; }
    }
}
