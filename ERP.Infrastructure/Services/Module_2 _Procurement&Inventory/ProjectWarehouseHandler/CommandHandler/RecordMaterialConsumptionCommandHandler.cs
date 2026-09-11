using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.CommandHandler
{
    public class RecordMaterialConsumptionCommandHandler : IRequestHandler<RecordMaterialConsumptionCommand, GeneralResponse<int>>
    {
        private readonly IProjectWarehouseStockRepository _projectWarehouseStockRepository;
        private readonly IProjectPhasesService _projectPhasesService;
        private readonly IMapper _mapper;

        public RecordMaterialConsumptionCommandHandler(IProjectWarehouseStockRepository projectWarehouseStockRepository,
                                                       IProjectPhasesService projectPhasesService , IMapper mapper)
        {
            _projectWarehouseStockRepository = projectWarehouseStockRepository;
            _projectPhasesService = projectPhasesService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<int>> Handle(RecordMaterialConsumptionCommand request, CancellationToken cancellationToken)
        {
            var projectPhase = await _projectPhasesService.GetById(request.ConsumptionDto.ProjectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<int>.Fail($"Project phase with ID {request.ConsumptionDto.ProjectPhaseId} not found.");

            if (projectPhase.FinishedDate != null)
                return GeneralResponse<int>.Fail($"Cannot consume material because project phase with ID {request.ConsumptionDto.ProjectPhaseId} is already finished.");

            if (!await _projectWarehouseStockRepository.IsMaterialExistAtProjectwarehouseStock(request.ConsumptionDto.MaterialId))
                return GeneralResponse<int>.Fail($"Material with id {request.ConsumptionDto.MaterialId} not found at projectwarehouse stock");


            var projectwarehouseMaterialStock = await _projectWarehouseStockRepository.GetByConditionAsync(s => s.MaterialId == request.ConsumptionDto.MaterialId
                                                                       && s.RemainingQuantity >= request.ConsumptionDto.RequestedQuantity);

            if (projectwarehouseMaterialStock is null)
                return GeneralResponse<int>.Fail($"Insufficient quantity of this material{request.ConsumptionDto.MaterialId} to fulfill the requested quantity.");


            projectwarehouseMaterialStock.RemainingQuantity -= request.ConsumptionDto.RequestedQuantity;

            var materialconsumption = _mapper.Map<MaterialConsumption>(request.ConsumptionDto);

            materialconsumption.Date = DateOnly.FromDateTime(DateTime.Now);
            materialconsumption.ProjectWarehouseStockId = projectwarehouseMaterialStock.Id;
            
            projectwarehouseMaterialStock.MaterialConsumptions?.Add(materialconsumption);
            
            await _projectWarehouseStockRepository.UpdateAsync(projectwarehouseMaterialStock);

            return GeneralResponse<int>.Success(materialconsumption.Id, "Material consumption recorded successfully.");

        }
    }
}
