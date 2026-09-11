using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Command
{
    public record CreateMaterialRequirementCommand( int ProjectPhaseId, CreateMaterialRequirementDto Dto): IRequest<GeneralResponse<Guid>>;
}
