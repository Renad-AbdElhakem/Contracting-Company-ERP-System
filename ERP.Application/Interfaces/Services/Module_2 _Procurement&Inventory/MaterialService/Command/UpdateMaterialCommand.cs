using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Command
{
    public record UpdateMaterialCommand(Guid MaterialId,UpdateMaterialDto MaterialDto) : IRequest<GeneralResponse<bool>>;
}
