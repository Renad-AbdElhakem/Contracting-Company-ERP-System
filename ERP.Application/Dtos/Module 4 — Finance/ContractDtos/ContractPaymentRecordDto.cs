using ERP.Domain.Model;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.ContractDtos
{
    public class ContractPaymentRecordDto
    {
        public Guid Id { get; set; }
        public decimal AmountDue { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? PaidDate { get; set; }
        public Guid ContractId { get; set; }
        public int ContractInstallmentPlanId { get; set; }
   
    }
}
