using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_3___Equipment_Machinery
{
    public class UpdateMaintenanceDto
    {
        public DateOnly? StartDate { get; set; }
        public string? Note { get; set; }
    }
}
