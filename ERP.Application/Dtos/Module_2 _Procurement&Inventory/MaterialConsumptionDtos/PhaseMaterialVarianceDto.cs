using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos
{
    public class PhaseMaterialVarianceDto
    {
        public int ProjectPhaseId { get; set; }
        public List<MaterialVarianceItemDto> Items { get; set; }
    }
}
