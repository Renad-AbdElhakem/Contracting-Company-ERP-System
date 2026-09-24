using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_2__Procurement_Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectOrderRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectOrderRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("{orderId}/materials")]
        public async Task<ActionResult<GeneralResponse<bool>>> AddNewMaterialToOrderCommand(int orderId, RequestOrderMaterialsDto orderMaterialsDto)
        {
            var result = await _mediator.Send(new AddMaterialToOrderCommand(orderId, orderMaterialsDto));
          
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        [HttpPatch("{orderRequestId}/validate")]
        public async Task<ActionResult<GeneralResponse<bool>>> ValidateOrderRequest(int orderRequestId)
        {
            var result = await _mediator.Send(new ValidateOrderRequestCommand(orderRequestId));
          
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpDelete("{orderRequestId}/Cancelled")]
        public async Task<ActionResult<GeneralResponse<bool>>> CancelledOrderRequest(int orderRequestId)
        {
            var result = await _mediator.Send(new CancelOrderRequestCommand(orderRequestId));
          
            return result.IsSuccess ? NoContent() : NotFound(result.Message);

        }

        [HttpPatch("{orderRequestId}/under-review")]
        public async Task<ActionResult<GeneralResponse<bool>>> SetOrderRequestUnderReview(int orderRequestId)
        {
            var result = await _mediator.Send(new SetOrderRequestUnderReviewCommand(orderRequestId));

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }


        [HttpGet("{orderRequestId}")]
        public async Task<ActionResult<GeneralResponse<OrderRequestDto>>> GetOrderRequestById(int orderRequestId)
        {
            var result = await _mediator.Send(new GetOrderRequestByIdQuery(orderRequestId));

            return Ok(result);
        }









    }
}
