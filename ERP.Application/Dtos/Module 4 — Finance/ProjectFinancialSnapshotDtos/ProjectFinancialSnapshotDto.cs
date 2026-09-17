using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos
{
    public class ProjectFinancialSnapshotDto
    {
        public Guid Id { get; set; }
        public DateOnly SnapshotDate { get; set; }
        public decimal ActualRevenue { get; set; }
        public decimal ActualCost { get; set; }
        public decimal ActualProfitLoss { get; set; }
       
    }
}
