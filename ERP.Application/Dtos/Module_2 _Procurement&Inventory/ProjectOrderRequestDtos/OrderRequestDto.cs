using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos
{
    public class OrderRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateOnly RequestDate { get; set; }
        public RequestStatus Status { get; set; }
        public Guid ProjectId { get; set; }
        public int RequestByEmployeeId { get; set; }
        public List<OrderMaterialDto> OrderMaterials { get; set; } = new();
    }
}
