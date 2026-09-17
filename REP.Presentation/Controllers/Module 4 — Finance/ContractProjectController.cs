using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_4___Finance
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContractProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("{contractId}/installments")]
        public async Task<ActionResult<GeneralResponse<int>>> AddInstallmentPlan(Guid contractId, List<CreateInstallmentPlanDto> createInstallmentPlanDtos)
        {
            var result = await _mediator.Send(new AddInstallmentPlanCommand(contractId, createInstallmentPlanDtos));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("{contractId}/installment-plans")]
        public async Task<ActionResult<GeneralResponse<List<ContractInstallmentPlanDto>>>> GetInstallmentPlanByContract(Guid contractId)
        {
            var result = await _mediator.Send(new GetInstallmentPlanByContractQuery(contractId));
        
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpGet("{contractId}/payments")]
        public async Task<ActionResult<GeneralResponse<List<ContractPaymentRecordDto>>>> GetPaymentRecordsByContract(Guid contractId)
        {
            var result = await _mediator.Send(new GetPaymentRecordsByContractQuery(contractId));
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet("{contractId}/late-payments-count")]
        public async Task<ActionResult<int>> GetLatePaymentsCount(Guid contractId)
        {
            var result = await _mediator.Send(new GetLatePaymentsCountQuery(contractId));
            return Ok(result);
        }


        [HttpGet("{projectId}/total-paid")]
        public async Task<IActionResult> GetSumContractPaymentRecordsByProjectId(Guid projectId)
        {
            var result = await _mediator.Send(new GetSumContractPaymentRecordsByProjectIdQuery(projectId));

            return Ok(result);
        }












    }
}
