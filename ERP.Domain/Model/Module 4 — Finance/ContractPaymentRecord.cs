using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class ContractPaymentRecord
    {
        public Guid Id { get; set; }
        public decimal AmountDue { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateOnly DueDate { get; set; }
        public DateOnly? PaidDate { get; set; }
        
        //Navigation
        public Guid ContractId { get; set; }
        public ContractProject ContractProject { get; set; }
    }
}
