using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierMaterialPriceService.Command
{
    public record DeleteMaterialPriceCommand(Guid Id) : IRequest<GeneralResponse<bool>>;
}
