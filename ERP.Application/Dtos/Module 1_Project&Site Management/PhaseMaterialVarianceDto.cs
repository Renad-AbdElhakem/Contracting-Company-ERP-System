using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_1_Project_Site_Management
{
    public class PhaseMaterialVarianceDto
    {
        public Guid MaterialId { get; set; }
        public string MaterialName { get; set; } = string.Empty;
        public decimal QuantityRequiremented { get; set; }
        public decimal QuantityUsed { get; set; }

    }
}
