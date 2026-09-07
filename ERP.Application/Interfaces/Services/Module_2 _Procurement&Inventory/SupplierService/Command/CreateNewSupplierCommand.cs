using ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Command
{
    public record CreateNewSupplierCommand(CreateNewSupplierDto NewSupplierDto) : IRequest<GeneralResponse<int>>;


}
