using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Query;
using ERP.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaterialsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Guid>>> Create(CreateMaterialDto newMaterialDto)
        {
            var response = await _mediator.Send(new CreateMaterialCommand(newMaterialDto));

            return !response.IsSuccess ? BadRequest(response.Message) : CreatedAtAction(nameof(GetById),
                                                                        new { materialId = response.Data },
                                                                        response.Data);
        }

        [HttpPut("{materialId}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Update(Guid materialId, UpdateMaterialDto materialDto)
        {
            var response = await _mediator.Send(new UpdateMaterialCommand(materialId, materialDto));

            return !response.IsSuccess ? BadRequest(response.Message) : Ok(response);
        }

        [HttpGet("{materialId}")]
        public async Task<ActionResult<GeneralResponse<MaterialDto>>> GetById(Guid materialId)
        {
            var response = await _mediator.Send(new GetMaterialByIdQuery(materialId));

            return !response.IsSuccess ? NotFound(response.Message) : Ok(response);
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<GeneralResponse<List<MaterialDto>>>> GetAllByFilter([FromQuery] MaterialType? type, [FromQuery] string? unit)
        {
            var response = await _mediator.Send(new GetAllMaterialsByFilterQuery(type, unit));

            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<MaterialDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllMaterialsQuery());

            return Ok(response.Data);
        }
       
        [HttpGet("{materialId}/best-price")]
        public async Task<ActionResult<GeneralResponse<List<MaterialDto>>>> BestMaterialprice(Guid materialId)
        {
            var response = await _mediator.Send(new GetBestPriceForMaterialQuery(materialId));

            return Ok(response.Data);
        }

    }
}
