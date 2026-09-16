using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Infrastructure.Repository.Module_1_Project_Site_Management;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.QueryHandler
{
    public class GetPhaseCostVarianceQueryHandler : IRequestHandler<GetPhaseCostVarianceQuery, GeneralResponse<ProjectPhaseCostVarianceDto>>
    {
        private readonly IProjectPhasesService _projectPhasesService;
        private readonly IMaterialPurchaseRepository _materialPurchaseRepository;

        public GetPhaseCostVarianceQueryHandler(IProjectPhasesService projectPhasesService,IMaterialPurchaseRepository materialPurchaseRepository)
        {
            _projectPhasesService = projectPhasesService;
           _materialPurchaseRepository = materialPurchaseRepository;
        }
        public async Task<GeneralResponse<ProjectPhaseCostVarianceDto>> Handle(GetPhaseCostVarianceQuery request, CancellationToken cancellationToken)
        {
            var projectPhase = await _projectPhasesService.GetByIdAsync(request.projectPhaseId);
            if (projectPhase is null)
                return GeneralResponse<ProjectPhaseCostVarianceDto>.Fail($"ProjectPhase with id {request.projectPhaseId} not found");

            var consumptions = await _projectPhasesService.GetProjectPhaseWithMaterialConsumptionsAsync(request.projectPhaseId);
           
            if (!consumptions.IsSuccess)
                return GeneralResponse<ProjectPhaseCostVarianceDto>.Fail(consumptions.Message);
           
            var materialIds = consumptions.Data.MaterialConsumptions.Select(c => c.MaterialId).Distinct().ToList();
            
            var expensesTotal = await _projectPhasesService.GetTotalExpensesByProjectPhaseIdAsync(request.projectPhaseId);
         
            var purchaseItems = await _materialPurchaseRepository.GetMaterialPurchaseItemsByMaterialIds(materialIds);

            var averagePricesByMaterial = purchaseItems
                .GroupBy(item => item.MaterialId)
                .ToDictionary(g => g.Key, g => g.Average(item => item.UnitPrice));
          
            decimal materialsCost = 0;
         
            foreach (var consumption in consumptions.Data.MaterialConsumptions)
            {
                if (averagePricesByMaterial.TryGetValue(consumption.MaterialId, out var avgUnitPrice))
                    materialsCost += avgUnitPrice * consumption.QuantityUsed;
            }

        
            var actualCost = expensesTotal + materialsCost;

            projectPhase.ActualCost = actualCost;
            projectPhase.ActualCostCalculatedAt = DateTime.Now;

            await _projectPhasesService.UpdateAsync(projectPhase);

            var result = new ProjectPhaseCostVarianceDto
            {
                EstimatedCost = projectPhase.EstimatedCost,
                ActualCost = actualCost,
                Variance = projectPhase.EstimatedCost - actualCost
            };

            return GeneralResponse<ProjectPhaseCostVarianceDto>.Success(result);
        }
    }
}
