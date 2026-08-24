using ERP.Domain.Model.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Equipment_Maintenance
    {
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly ?EndDate { get; set; }
        public string? Note { get; set; }
        public decimal? Cost { get; set; }
        //Navigation 
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public ICollection<MaintenanceEmployee> MaintenanceEmployees { get; set; } = new List<MaintenanceEmployee>();
    }
}
