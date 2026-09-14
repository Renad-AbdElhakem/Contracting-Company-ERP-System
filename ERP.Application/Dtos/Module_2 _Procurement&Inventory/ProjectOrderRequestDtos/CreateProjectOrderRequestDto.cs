using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos
{
    public class CreateProjectOrderRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RequestByEmployeeId { get; set; }
        public ICollection<RequestOrderMaterialsDto> orderMaterialsDtos { get; set; } = new List<RequestOrderMaterialsDto>();

    }
}
