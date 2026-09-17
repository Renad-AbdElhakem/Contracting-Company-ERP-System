using ERP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.ContractDtos
{
    public class AllProjectContractDetailsDto
    {
        public Guid ContractId { get; set; }
        public DateOnly SignDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PenaltyClauseNote { get; set; }
        public decimal? PenaltyPerDay { get; set; }
        public decimal? MaxPenaltyAmount { get; set; }
        public ContractStatus Status { get; set; }
        public Guid ClientId { get; set; }
        public Guid ProjectId { get; set; }
        public List<ContractPaymentRecordDto> ContractPaymentRecords { get; set; } = new();
        public List<ContractInstallmentPlanDto> ContractPaymentPlans { get; set; } = new();
    }
}
