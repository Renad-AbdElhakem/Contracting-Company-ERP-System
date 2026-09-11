using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Command
{
    public record DeleteMaterialRequirementCommand(Guid MaterialRequirementId) : IRequest<GeneralResponse<bool>>;

}
