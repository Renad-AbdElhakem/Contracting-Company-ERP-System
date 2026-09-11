using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialRequirementsHandler.CommandHandler
{
    public class UpdateMaterialRequirementCommandHandler
     : IRequestHandler<UpdateMaterialRequirementCommand, GeneralResponse<bool>>
    {
        private readonly IPhaseMaterialRequirementRepository _materialRequirementRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public UpdateMaterialRequirementCommandHandler(IPhaseMaterialRequirementRepository materialRequirementRepository, IMaterialRepository materialRepository,
            IMapper mapper)
        {
            _materialRequirementRepository = materialRequirementRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<bool>> Handle(UpdateMaterialRequirementCommand request,CancellationToken cancellationToken)
        {
            var materialRequirement = await _materialRequirementRepository.GetByIdAsync(request.MaterialRequirementId);

            if (materialRequirement is null)
                return GeneralResponse<bool>.Fail($"Material requirement with id {request.MaterialRequirementId} not found.");

            if (request.Dto.MaterialId.HasValue)
            {
                var material = await _materialRepository.GetByIdAsync(request.Dto.MaterialId.Value);

                if (material is null)
                    return GeneralResponse<bool>.Fail( $"Material with id {request.Dto.MaterialId} not found.");
            }

            if (request.Dto.Quantity.HasValue)
                materialRequirement.Quantity = request.Dto.Quantity.Value;

            if (request.Dto.Notes != null)
                materialRequirement.Notes = request.Dto.Notes;

            if (request.Dto.MaterialId.HasValue)
                materialRequirement.MaterialId = request.Dto.MaterialId.Value;

            await _materialRequirementRepository.UpdateAsync(materialRequirement);

            return GeneralResponse<bool>.Success( true, "Material requirement updated successfully.");
        }
    }
}
