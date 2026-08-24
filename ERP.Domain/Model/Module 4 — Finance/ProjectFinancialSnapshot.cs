using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_4___Finance
{
    public class ProjectFinancialSnapshot
    {
        public Guid Id { get; set; }
        public DateOnly SnapshotDate { get; set; }
        public decimal ActualRevenue { get; set; }
        public decimal ActualCost { get; set; }
        public decimal ActualProfitLoss { get; set; }   
        public int CreatedByEmployeeId { get; set; }
        public Employee Employee { get; set; }

        // Navigation
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
