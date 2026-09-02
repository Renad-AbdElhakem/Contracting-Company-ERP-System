using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_3___Equipment_Machinery
{
    public class MaintenanceDto
    {
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Note { get; set; }
        public decimal? Cost { get; set; }
        public Guid EquipmentId { get; set; }
    }
}
