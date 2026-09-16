using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.Module_4___Finance
{
    public class ContractInstallmentPlan
    {
        public int Id { get; set; }
        public int InstallmentNumber { get; set; }
        public decimal Percentage { get; set; }
        public int MonthsAfterSignDate { get; set; }

        // Navigation
        public Guid ContractId { get; set; }
        public ContractProject ContractProject { get; set; }
      public  ICollection<ContractPaymentRecord>? ContractPaymentRecords { get; set; } = new List<ContractPaymentRecord>();
    }
}
