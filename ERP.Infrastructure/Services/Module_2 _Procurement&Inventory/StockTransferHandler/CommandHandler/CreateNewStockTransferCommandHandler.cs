using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.StockTransferService.Command;
using ERP.Domain.Enum;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using ERP.Infrastructure.Repository.Module_2__Procurement_Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.StockTransferHandler.CommandHandler
{
    public class CreateNewStockTransferCommandHandler : IRequestHandler<CreateNewStockTransferCommand, GeneralResponse<int>>
    {
        private readonly IProjectWarehouseRepository _projectWarehouseRepository;
        private readonly IStockTransferRepository _stockTransferRepository;
        private readonly IMapper _mapper;

        public CreateNewStockTransferCommandHandler(IProjectWarehouseRepository projectWarehouseRepository, IStockTransferRepository stockTransferRepository
            , IMapper mapper)
        {
            _projectWarehouseRepository = projectWarehouseRepository;
            _stockTransferRepository = stockTransferRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<int>> Handle(CreateNewStockTransferCommand request, CancellationToken cancellationToken)
        {

            var availableProjectWarehouse = await _projectWarehouseRepository.GetByIdWithCondition(pw => !pw.IsFull);

            if (availableProjectWarehouse is null)
                return GeneralResponse<int>.Fail("No available project warehouse found");

            var newStockTransfer = _mapper.Map<List<StockTransfer>>(request.newStockTransferDtos);

            newStockTransfer.ForEach(stockTransfer =>
            {
                stockTransfer.ProjectWarehouseId = availableProjectWarehouse.Id;
                stockTransfer.SentDate = DateTime.Now;
                stockTransfer.Status = StockTransferStatus.InTransit;
            });

           await _stockTransferRepository.AddRangeAsync(newStockTransfer);
            return GeneralResponse<int>.Success();
        }
    }
}
