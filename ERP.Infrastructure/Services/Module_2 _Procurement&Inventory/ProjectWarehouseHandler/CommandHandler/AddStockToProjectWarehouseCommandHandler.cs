using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
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

        public AddStockToProjectWarehouseCommandHandler(IMapper mapper,IProjectWarehouseRepository warehouseRepository,
                                                         IMaterialRepository materialRepository, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _warehouseRepository = warehouseRepository;
            _materialRepository = materialRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<GeneralResponse<int>> Handle(AddStockToProjectWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(request.warehouseId);

            if (warehouse == null)
                return GeneralResponse<int>.Fail($"Project warehouse with id {request.warehouseId} not found");

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

            warehouse.ProjectWarehouseStocks.Add(stock);
          
            await _warehouseRepository.UpdateAsync(warehouse);

            return GeneralResponse<int>.Success(stock.Id);
        }
    }
}
