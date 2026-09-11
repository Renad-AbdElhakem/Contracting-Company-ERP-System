using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialRequirementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaterialRequirementsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPatch("{materialRequirementId}")]
        public async Task<IActionResult> Update(Guid materialRequirementId, UpdateMaterialRequirementDto dto)
        {
            var result = await _mediator.Send(new UpdateMaterialRequirementCommand(materialRequirementId, dto));

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{materialRequirementId}")]
        public async Task<IActionResult> Delete(Guid materialRequirementId)
        {
            var result = await _mediator.Send(new DeleteMaterialRequirementCommand(materialRequirementId));

            return result.IsSuccess ? NoContent() : BadRequest(result);
        }
       
        [HttpGet("{materialRequirementId}")]
        public async Task<IActionResult> GetMaterialRequirementById(Guid materialRequirementId)
        {
            var result = await _mediator.Send(new GetMaterialRequirementByIdQuery(materialRequirementId));

            return result.IsSuccess ? Ok(result): NotFound(result);
        }
    }
}
