using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos
{
    public class RecordMaterialPurchasePaymentDto
    {
        public decimal AmountPaid { get; set; }
        public DateOnly PaidDate { get; set; }
        public DateOnly DueDate { get; set; }
    }
}
