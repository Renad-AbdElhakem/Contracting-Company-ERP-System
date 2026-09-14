using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query
{
    public record GetOrderRequestByIdQuery(int orderRequestId) : IRequest<GeneralResponse<OrderRequestDto>>;
}
