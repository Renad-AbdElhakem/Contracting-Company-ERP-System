using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_4___Finance
{
    [Route("api/[controller]")]
    [ApiController]
    public class paymentrecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public paymentrecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPatch("{paymentRecordId}/pay")]
        public async Task<IActionResult> RecordPayment(Guid paymentRecordId, AddPaymentRecordDto addPaymentRecordDto)
        {
            var result = await _mediator.Send(new RecordPaymentCommand(paymentRecordId, addPaymentRecordDto));
           return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
