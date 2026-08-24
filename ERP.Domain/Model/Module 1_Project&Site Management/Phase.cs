using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Phase
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;

        public ICollection<ProjectPhase> ?ProjectPhases { get; set; } = new List<ProjectPhase>();
    }
}
