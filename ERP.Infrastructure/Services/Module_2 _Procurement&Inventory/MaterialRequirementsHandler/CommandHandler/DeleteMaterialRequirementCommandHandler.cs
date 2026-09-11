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
    public class DeleteMaterialRequirementCommandHandler : IRequestHandler<DeleteMaterialRequirementCommand, GeneralResponse<bool>>
    {
        private readonly IPhaseMaterialRequirementRepository _materialRequirementRepository;

        public DeleteMaterialRequirementCommandHandler(
            IPhaseMaterialRequirementRepository materialRequirementRepository)
        {
            _materialRequirementRepository = materialRequirementRepository;
        }

        public async Task<GeneralResponse<bool>> Handle( DeleteMaterialRequirementCommand request, CancellationToken cancellationToken)
        {
            var materialRequirement = await _materialRequirementRepository.GetByIdAsync(request.MaterialRequirementId);

            if (materialRequirement is null)
                return GeneralResponse<bool>.Fail($"Material requirement with id {request.MaterialRequirementId} not found.");

            await _materialRequirementRepository.DeleteAsync(materialRequirement);

            return GeneralResponse<bool>.Success(true,"Material requirement deleted successfully.");
        }
    }
}
