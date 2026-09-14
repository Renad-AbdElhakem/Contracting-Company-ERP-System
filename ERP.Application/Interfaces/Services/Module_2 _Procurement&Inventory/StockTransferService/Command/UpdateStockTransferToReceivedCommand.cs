using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.StockTransferService.Command
{
    public record UpdateStockTransferToReceivedCommand(int stockTransferId):IRequest<GeneralResponse<bool>>;
    
}
