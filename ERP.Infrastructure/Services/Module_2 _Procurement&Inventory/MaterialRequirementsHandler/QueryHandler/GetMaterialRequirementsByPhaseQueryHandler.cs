using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialRequirementsHandler.QueryHandler
{
    public class GetMaterialRequirementsByPhaseQueryHandler : IRequestHandler<GetMaterialRequirementsByPhaseQuery,GeneralResponse<List<MaterialRequirementDto>>>
    {
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IMapper _mapper;

        public GetMaterialRequirementsByPhaseQueryHandler(IProjectPhaseRepository projectPhaseRepository, IMapper mapper)
        {
            _projectPhaseRepository = projectPhaseRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<MaterialRequirementDto>>> Handle(GetMaterialRequirementsByPhaseQuery request, CancellationToken cancellationToken)
        {
            var projectphase = await _projectPhaseRepository.GetByIdWithInclude(request.ProjectPhaseId,x => x.ProjectMaterials);

            if (projectphase is null)
                return GeneralResponse<List<MaterialRequirementDto>>.Fail($"Project phase with id {request.ProjectPhaseId} not found.");

            var result = _mapper.Map<List<MaterialRequirementDto>>(projectphase.ProjectMaterials);

            return GeneralResponse<List<MaterialRequirementDto>>.Success(result, "Material requirements retrieved successfully.");
        }
    }
}
