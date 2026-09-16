using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos
{
    public class ProjectPhaseCostVarianceDto
    {
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public decimal Variance { get; set; }
        public DateTime ActualCostCalculatedAt { get; set; }
    }
}
