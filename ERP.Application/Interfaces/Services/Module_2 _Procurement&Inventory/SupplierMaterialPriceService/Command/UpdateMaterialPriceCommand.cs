using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierMaterialPriceService.Command
{
    public record UpdateMaterialPriceCommand(Guid Id, UpdateMaterialPriceDto MaterialPriceDto) : IRequest<GeneralResponse<bool>>;
}
