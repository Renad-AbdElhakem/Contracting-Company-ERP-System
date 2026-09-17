using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.QueryHandler
{
    public class GetPhaseMaterialVarianceQueryHandler : IRequestHandler<GetPhaseMaterialVarianceQuery, GeneralResponse<PhaseMaterialVarianceDto>>
    {
        private readonly IProjectPhaseRepository _projectPhaseRepository;

        public GetPhaseMaterialVarianceQueryHandler(IProjectPhaseRepository projectPhaseRepository)
        {
            _projectPhaseRepository = projectPhaseRepository;
        }
        public async Task<GeneralResponse<PhaseMaterialVarianceDto>> Handle(GetPhaseMaterialVarianceQuery request, CancellationToken cancellationToken)
        {
            var projectPhase = await _projectPhaseRepository.GetAllMaterialVarianceByProjectPhaseIdAsync(request.projectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<PhaseMaterialVarianceDto>.Fail($"ProjectPhase with id {request.projectPhaseId} not found");

            // Planned: from PhaseMaterialRequirement
            var plannedByMaterial = projectPhase.ProjectMaterials
                .GroupBy(pm => pm.MaterialId)
                .ToDictionary(g => g.Key, g => g.Sum(pm => pm.Quantity));

            // Actual: from MaterialConsumption
            var actualByMaterial = projectPhase.MaterialConsumptions
                .GroupBy(mc => mc.MaterialId)
                .ToDictionary(g => g.Key, g => g.Sum(mc => mc.QuantityUsed));

            // Union of both sets — a material might be planned but never consumed, or vice versa
            var allMaterialIds = plannedByMaterial.Keys.Union(actualByMaterial.Keys).Distinct();

            var items = allMaterialIds.Select(materialId =>
            {
                var material = projectPhase.ProjectMaterials
                    .FirstOrDefault(pm => pm.MaterialId == materialId)?.Material;

                var planned = plannedByMaterial.GetValueOrDefault(materialId, 0);
                var actual = actualByMaterial.GetValueOrDefault(materialId, 0);

                return new MaterialVarianceItemDto
                {
                    MaterialId = materialId,
                    MaterialName = material?.Name ?? "N/A",
                    PlannedQuantity = planned,
                    ActualQuantity = actual,
                    Variance = planned - actual
                };
            }).ToList();

            var result = new PhaseMaterialVarianceDto
            {
                ProjectPhaseId = projectPhase.Id,
                Items = items
            };

            return GeneralResponse<PhaseMaterialVarianceDto>.Success(result);
        }
    }
}
