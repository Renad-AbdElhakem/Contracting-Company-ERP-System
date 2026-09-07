using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierMaterialPriceService.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class suppliermaterialpricesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public suppliermaterialpricesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Update(Guid id,UpdateMaterialPriceDto dto)
        {
            var response = await _mediator.Send(new UpdateMaterialPriceCommand(id, dto));

            return !response.IsSuccess ? BadRequest(response.Message) : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteMaterialPriceCommand(id));

            return !response.IsSuccess ? NotFound(response.Message) : Ok(response);
        }
    }
}
