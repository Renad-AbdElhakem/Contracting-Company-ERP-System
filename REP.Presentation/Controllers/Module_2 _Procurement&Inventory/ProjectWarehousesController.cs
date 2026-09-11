using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectWarehousesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectWarehousesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{warehouseId}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Update(int warehouseId, UpdateProjectWarehouseDto warehouseDto)
        {
            var response = await _mediator.Send(new UpdateProjectWarehouseCommand(warehouseId, warehouseDto));

            return !response.IsSuccess ? BadRequest(response.Message) : Ok(response);
        }

        [HttpGet("{warehouseId}")]
        public async Task<ActionResult<GeneralResponse<ProjectWarehouseDto>>> GetById(int warehouseId)
        {
            var response = await _mediator.Send(new GetProjectWarehouseByIdQuery(warehouseId));

            return !response.IsSuccess ? NotFound(response.Message) : Ok(response.Data);
        }
        [HttpPost("{warehouseId}/stock")]
        public async Task<ActionResult<GeneralResponse<int>>> AddToProjectWarehouse(int warehouseId, AddProjectWarehouseStockDto stockDto)
        {
            var response = await _mediator.Send(new AddStockToProjectWarehouseCommand(warehouseId, stockDto));

            return !response.IsSuccess ? BadRequest(response.Message) : Ok(response);
        }


        [HttpGet("{warehouseId}/stock")]
        public async Task<ActionResult<GeneralResponse<List<ProjectWarehouseStockDto>>>> GetProjectWarehouseStock(int warehouseId)
        {
            var response = await _mediator.Send(new GetProjectWarehouseStockQuery(warehouseId));

            return !response.IsSuccess ? NotFound(response.Message) : Ok(response);
        }



        [HttpPost("computed")]
        public async Task<ActionResult<GeneralResponse<int>>> AddToProjectWarehouse(RequestedMaterialConsumptionDto materialConsumptionDto)
        {
            var response = await _mediator.Send(new RecordMaterialConsumptionCommand(materialConsumptionDto));

            return response.IsSuccess ? Ok(response):BadRequest(response.Message);

        }





    }
}
