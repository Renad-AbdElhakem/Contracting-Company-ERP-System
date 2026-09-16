using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_4___Finance
{
    [Route("api/[controller]")]
    [ApiController]
    public class materialpurchasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public materialpurchasesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{materialPurchaseId}")]
        public async Task<ActionResult<GeneralResponse<MaterialPurchaseDto>>> GetMaterialPurchaseById(Guid materialPurchaseId)
        {
            var response = await _mediator.Send(new GetMaterialPurchaseByIdQuery(materialPurchaseId));
              return Ok(response);
        }

        [HttpPost("{materialPurchaseId}/payments")]
        public async Task<ActionResult<GeneralResponse<Guid>>> RecordMaterialPurchasePayment(Guid materialPurchaseId, RecordMaterialPurchasePaymentDto dto)
        {
            var response = await _mediator.Send(new RecordMaterialPurchasePaymentCommand(materialPurchaseId,dto));
              return response.IsSuccess?Ok(response):BadRequest(response.Message);
        }
    }
}
