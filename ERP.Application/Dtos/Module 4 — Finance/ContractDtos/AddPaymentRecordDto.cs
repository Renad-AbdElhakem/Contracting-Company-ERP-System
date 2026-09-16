using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.ContractDtos
{
    public class AddPaymentRecordDto
    {
        public decimal? AmountPaid { get; set; }
        public DateOnly? PaidDate { get; set; }
    }
}
