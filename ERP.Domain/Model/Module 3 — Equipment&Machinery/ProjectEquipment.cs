using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_3___Equipment_Machinery
{
    public class ProjectEquipment
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
      
        //Navigation

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}
