using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos
{
    public class UpdateMaterialRequirementDto
    {
        public decimal? Quantity { get; set; }
        public string? Notes { get; set; }
        public Guid? MaterialId { get; set; }
    }
}
