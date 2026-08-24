using ERP.Domain.Enum;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class ProjectOrderRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateOnly RequestDate { get; set; }
        public RequestStatus Status {  get; set; }

        //Navigation 
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public int RequestByEmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<OrderMaterials> ?OrderMaterials { get; set; }= new List<OrderMaterials>();
    }
}
