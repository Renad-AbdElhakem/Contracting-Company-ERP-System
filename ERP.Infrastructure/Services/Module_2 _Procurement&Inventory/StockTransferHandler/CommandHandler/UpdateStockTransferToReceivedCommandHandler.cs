using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.StockTransferService.Command;
using ERP.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.StockTransferHandler.CommandHandler
{
    public class UpdateStockTransferToReceivedCommandHandler : IRequestHandler<UpdateStockTransferToReceivedCommand, GeneralResponse<bool>>
    {
        private readonly IStockTransferRepository _stockTransferRepository;

        public UpdateStockTransferToReceivedCommandHandler(IStockTransferRepository stockTransferRepository)
        {
            _stockTransferRepository = stockTransferRepository;
        }
        public async Task<GeneralResponse<bool>> Handle(UpdateStockTransferToReceivedCommand request, CancellationToken cancellationToken)
        {
            var stockTransfer = await _stockTransferRepository.GetByIdAsync(request.stockTransferId);
         
            if (stockTransfer is null)
                return GeneralResponse<bool>.Fail($"Stock transfer with id {request.stockTransferId} not found");
            
            stockTransfer.ReceivedDate = DateTime.Now;
            stockTransfer.Status = StockTransferStatus.Received;

            await _stockTransferRepository.UpdateAsync(stockTransfer);
            return GeneralResponse<bool>.Success(true, "Stock transfer record updated successfully");

        }
    }
}
