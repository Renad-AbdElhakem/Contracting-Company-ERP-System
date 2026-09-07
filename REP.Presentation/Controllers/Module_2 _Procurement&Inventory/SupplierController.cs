using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Queries;
using ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler;
using ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler.QueryHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<GeneralResponse<int>>> CreateNew(CreateNewSupplierDto newSupplierDto)
        {
            var response = await _mediator.Send(new CreateNewSupplierCommand(newSupplierDto));

            return !response.IsSuccess ? BadRequest(response.Message) : CreatedAtAction(nameof(GetById),
                                                                new { supplierId = response.Data }, null);
        }



        [HttpGet("{supplierId}")]
        public async Task<ActionResult<GeneralResponse<SupplierDto>>> GetById(int supplierId)
        {
            var response = await _mediator.Send(new GetSupplierByIdQuery(supplierId));
            return response.IsSuccess ? Ok(response.Data) : NotFound(response.Message);
        }

       
        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<SupplierDto>>>> GetAllBy()
        {
            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());
            return Ok(suppliers.Data);
        }

        [HttpDelete("{supplierId}")]
        public async Task<ActionResult> SoftDeleteSupplier(int supplierId, EndContractWithSupplierDto dto)
        {
            var response = await _mediator.Send(new EndContractWithSupplierCommand(supplierId, dto));
            return response.IsSuccess ? NoContent() : NotFound(response.Message);
        }


        [HttpPatch("{supplierId}")]
        public async Task<ActionResult> UpdateById(int supplierId, [FromQuery] UpdateSupplierDto updateSupplier)
        {
            var response = await _mediator.Send(new UpdateSupplierCommand(supplierId, updateSupplier));
            return response.IsSuccess ? NoContent() : NotFound(response.Message);
        }



        [HttpPost("{supplierId}/materialprices")]
        public async Task<ActionResult<GeneralResponse<Guid>>> AddNewSupplierMaterialprices(int supplierId, AddSupplierMaterialPriceDto dto)
        {

            var response = await _mediator.Send(new AddSupplierMaterialPriceCommand(supplierId, dto));
            return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
        }


        [HttpGet("{supplierId}/materialprices")]
        public async Task<ActionResult<GeneralResponse<List<SupplierDto>>>> GetMaterialPrices(int supplierId)
        {
            var suppliers = await _mediator.Send(new GetSupplierMaterialPricesQuery(supplierId));
            return Ok(suppliers.Data);
        }
           
        }
    }
