using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Command;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2___Procurement___Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialRequirementsHandler.CommandHandler
{
    public class CreateMaterialRequirementCommandHandler : IRequestHandler<CreateMaterialRequirementCommand, GeneralResponse<Guid>>
    {
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public CreateMaterialRequirementCommandHandler(IProjectPhaseRepository projectPhaseRepository, IMaterialRepository materialRepository
                                                        , IMapper mapper)
        {
            _projectPhaseRepository = projectPhaseRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<Guid>> Handle(CreateMaterialRequirementCommand request, CancellationToken cancellationToken)
        {
            var projectphase = await _projectPhaseRepository.GetByIdAsync(request.ProjectPhaseId);

            if (projectphase is null)
                return GeneralResponse<Guid>.Fail($"Project phase with id {request.ProjectPhaseId} not found.");

            var material = await _materialRepository.GetByIdAsync(request.Dto.MaterialId);

            if (material == null)
                return GeneralResponse<Guid>.Fail($"Material with id {request.Dto.MaterialId} not found.");

            var materialRequirements = _mapper.Map<PhaseMaterialRequirement>(request.Dto);

            projectphase.ProjectMaterials.Add(materialRequirements);
           
            await _projectPhaseRepository.UpdateAsync(projectphase);

            return GeneralResponse<Guid>.Success(materialRequirements.Id,"Material requirement created successfully.");
        }
    }
}
