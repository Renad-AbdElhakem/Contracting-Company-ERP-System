using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_4___Finance
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
           _mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Guid>>> CreateExpense(CreateExpenseDto dto)
        {
            var result = await _mediator.Send(new CreateExpenseCommand(dto));

            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);
          
        }





        [HttpGet]
        public async Task<ActionResult<GeneralResponse<List<ExpenseDto>>>> GetAllExpenses([FromQuery] ExpenseFilterDto filter)
        {
            var result = await _mediator.Send(new GetAllExpensesQuery(filter));
            return Ok(result);
        }











    }
}
