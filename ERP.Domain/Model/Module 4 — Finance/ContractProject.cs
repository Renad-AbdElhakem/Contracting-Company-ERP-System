using ERP.Domain.Enum;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class ContractProject
    {
        public Guid Id { get; set; }
        public DateOnly SignDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PenaltyClauseNote { get; set; }
        public decimal? PenaltyPerDay { get; set; }
        public decimal? MaxPenaltyAmount { get; set; }
        public ContractStatus Status { get; set; }
      

        //Navigation
        public Guid ClientId { get; set; }  
        public Client Client {  get; set;  }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<ContractPaymentRecord>?  ContractPaymentRecords { get; set; } = new List<ContractPaymentRecord>();
        public ICollection<ContractInstallmentPlan> ? ContractPaymentPlans { get; set; } = new List<ContractInstallmentPlan>();
    }
}
