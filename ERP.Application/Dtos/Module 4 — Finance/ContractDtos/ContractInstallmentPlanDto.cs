using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.ContractDtos
{
    public class ContractInstallmentPlanDto
    {
        public int Id { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal Percentage { get; set; }
        public int MonthsAfterSignDate { get; set; }
       
    }
}
