using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command
{
    public record GetProjectWarehousesQuery(Guid projectId)  : IRequest<GeneralResponse<List<ProjectWarehouseDto>>>;
}
