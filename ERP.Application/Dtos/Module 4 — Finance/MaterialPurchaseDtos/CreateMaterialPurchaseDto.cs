using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos
{
    public class CreateMaterialPurchaseDto
    {
        public DateOnly ArrivalDate { get; set; }
        public  List<CreateMaterialPurchaseItemDto> purchaseItemDtos { get; set; } = new List<CreateMaterialPurchaseItemDto>();
    }
}
