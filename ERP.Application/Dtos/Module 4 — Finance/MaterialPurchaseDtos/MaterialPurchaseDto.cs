using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos
{
    public class MaterialPurchaseDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public int SupplierId { get; set; }
        public List<MaterialPurchaseItemDto> MaterialPurchaseItems { get; set; } = new();
        public List<MaterialPurchasePaymentDto> Payments { get; set; } = new();
    }
}
