using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos
{
    public class CreateMaterialDto
    {
        public string Name { get; set; }
        public string Unit { get; set; } 
        public MaterialType MaterialType { get; set; }
    }
}
