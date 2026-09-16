using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Query
{
    public record GetAllExpensesQuery(ExpenseFilterDto Filter) : IRequest<GeneralResponse<List<ExpenseDto>>>;
}
