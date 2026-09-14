using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.StockTransferService.Command;
using ERP.Domain.Enum;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.CommandHandler
{
    public class AddStockToProjectWarehouseCommandHandler : IRequestHandler<AddStockToProjectWarehouseCommand, GeneralResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectWarehouseRepository _warehouseRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMediator _mediator;
      

        public AddStockToProjectWarehouseCommandHandler(IMapper mapper, IProjectWarehouseRepository warehouseRepository,
                                                         IMaterialRepository materialRepository,
                                                         IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _mapper = mapper;
            _warehouseRepository = warehouseRepository;
            _materialRepository = materialRepository;
            _employeeRepository = employeeRepository;
            _mediator = mediator;
        }

        public async Task<GeneralResponse<int>> Handle(AddStockToProjectWarehouseCommand request, CancellationToken cancellationToken)
        {

            var orderRequested = await _mediator.Send(new GetOrderRequestByIdQuery(request.orderRequestId));
          
            if (!orderRequested.IsSuccess)
                return GeneralResponse<int>.Fail(orderRequested.Message);

            var warehouse = await _warehouseRepository.GetByIdAsync(request.StockDto.ProjectWarehouseId);

            if (warehouse is null)
                return GeneralResponse<int>.Fail($"Project warehouse with id {request.orderRequestId} not found");

            var material = await _materialRepository.GetByIdAsync(request.StockDto.MaterialId);

            if (material == null)
                return GeneralResponse<int>.Fail($"Material with id {request.StockDto.MaterialId} not found");

            var employee = await _employeeRepository.GetByIdAsync(request.StockDto.ReceivedByEmployeeId);

            if (employee == null)
                return GeneralResponse<int>.Fail($"Employee with id {request.StockDto.ReceivedByEmployeeId} not found");

            if (employee.TerminationDate is not null)
                return GeneralResponse<int>.Fail($"Employee with id {request.StockDto.ReceivedByEmployeeId} has been terminated");

            if (employee.Status != EmployeeStatus.Active)
                return GeneralResponse<int>.Fail($"Employee with id {request.StockDto.ReceivedByEmployeeId} is not active");

            if (request.StockDto.Quantity <= 0)
                return GeneralResponse<int>.Fail("Quantity must be greater than zero");

            var stock = _mapper.Map<ProjectWarehouseStock>(request.StockDto);

            var checkOrderMaterialQuantity = orderRequested.Data.OrderMaterials.FirstOrDefault(m => m.MaterialId == request.StockDto.MaterialId);

            stock.RemainingQuantity = request.StockDto.Quantity;
            stock.ArrivalDate = DateTime.Now;
            stock.StockTransferId = request.StockDto.StockTransferId;

            stock.ReceivingStatus = checkOrderMaterialQuantity.Quantity > request.StockDto.Quantity ?
                                                       ReceivingStatus.Shortage : ReceivingStatus.Matched;


            warehouse.ProjectWarehouseStocks.Add(stock);

            await _warehouseRepository.UpdateAsync(warehouse);

            var result = await _mediator.Send(new UpdateStockTransferToReceivedCommand(request.StockDto.StockTransferId));

            if (!result.IsSuccess)
                return GeneralResponse<int>.Fail(result.Message);

            return GeneralResponse<int>.Success(stock.Id);
        }
    }
}
