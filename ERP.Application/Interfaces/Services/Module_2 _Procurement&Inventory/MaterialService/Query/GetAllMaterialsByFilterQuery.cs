using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using ERP.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Query
{
    public record GetAllMaterialsByFilterQuery(MaterialType? Type,string? Unit) : IRequest<GeneralResponse<List<MaterialDto>>>;
}
