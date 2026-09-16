using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Command
{
    public record CreateProjectExpenseCommand(int ProjectPhaseId, CreateProjectExpenseDto ExpenseDto ) : IRequest<GeneralResponse<Guid>>;
}
