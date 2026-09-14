using ERP.Application.Dtos.Module_2__Procurement_Inventory.StockTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.StockTransferService.Command
{
    public record CreateNewStockTransferCommand(List<CreateNewStockTransferDto> newStockTransferDtos):IRequest<GeneralResponse<int>>;
    
}
