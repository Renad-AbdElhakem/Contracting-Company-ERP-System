using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.CompanyWarehouseDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyWarehousesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyWarehousesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse<int>>> Create(CreateCompanyWarehouseDto dto)
        {
            var response = await _mediator.Send(new CreateCompanyWarehouseCommand(dto));

            return !response.IsSuccess ? BadRequest(response.Message) : CreatedAtAction(nameof(GetById), 
                                                                      new { companyWarehouseId = response.Data },response.Data);
        }

        [HttpPatch("{companyWarehouseId}")]
        public async Task<ActionResult<GeneralResponse<bool>>> Update(int companyWarehouseId, UpdateCompanyWarehouseDto dto)
        {
            var response = await _mediator.Send(new UpdateCompanyWarehouseCommand(companyWarehouseId, dto));

            return !response.IsSuccess ? BadRequest(response.Message) : Ok(response);
        }

        [HttpGet("{companyWarehouseId}")]
        public async Task<ActionResult<GeneralResponse<CompanyWarehouseDto>>> GetById(int companyWarehouseId)
        {
            var response = await _mediator.Send(new GetCompanyWarehouseByIdQuery(companyWarehouseId));

            return !response.IsSuccess ? NotFound(response.Message) : Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<CompanyWarehouseDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllCompanyWarehousesQuery());
            return Ok(response);
        }

        [HttpPost("{warehouseId}/stock")]
        public async Task<ActionResult<GeneralResponse<int>>> AddStockToCompanyWarehouse(int warehouseId, AddCompanyWarehouseStockDto stockDto)
        {
            var response = await _mediator.Send(new AddStockToCompanyWarehouseCommand(warehouseId, stockDto));

            return !response.IsSuccess ? BadRequest(response.Message) : Ok(response);
        }
        [HttpGet("{warehouseId}/stock")]
        public async Task<ActionResult<GeneralResponse<List<CompanyWarehouseStockDto>>>> GetCompanyWarehouseStock(int warehouseId)
        {
            var response = await _mediator.Send(new GetCompanyWarehouseStockQuery(warehouseId));

            return !response.IsSuccess ? NotFound(response.Message) : Ok(response);
        }
    }
}
