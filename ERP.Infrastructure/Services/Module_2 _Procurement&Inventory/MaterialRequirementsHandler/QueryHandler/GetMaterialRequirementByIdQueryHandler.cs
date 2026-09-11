using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialRequirementsHandler.QueryHandler
{
    public class GetMaterialRequirementByIdQueryHandler : IRequestHandler<  GetMaterialRequirementByIdQuery, GeneralResponse<MaterialRequirementDto>>
    {
        private readonly IPhaseMaterialRequirementRepository _repository;
        private readonly IMapper _mapper;

        public GetMaterialRequirementByIdQueryHandler( IPhaseMaterialRequirementRepository repository,  IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<MaterialRequirementDto>> Handle(GetMaterialRequirementByIdQuery request,  CancellationToken cancellationToken)
        {
            var materialRequirement = await _repository.GetByIdAsync(request.MaterialRequirementId);

            if (materialRequirement is null)
                return GeneralResponse<MaterialRequirementDto>.Fail($"Material requirement with id {request.MaterialRequirementId} not found.");

            var result = _mapper.Map<MaterialRequirementDto>( materialRequirement);

            return GeneralResponse<MaterialRequirementDto>.Success(result,"Material requirement retrieved successfully.");
        }
    }
}
