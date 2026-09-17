using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos
{
    public class MaterialVarianceItemDto
    {
        public Guid MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal PlannedQuantity { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal Variance { get; set; } 
    }
}
