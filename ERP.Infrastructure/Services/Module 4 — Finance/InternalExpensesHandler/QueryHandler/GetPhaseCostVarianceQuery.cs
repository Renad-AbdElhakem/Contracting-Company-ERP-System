using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.QueryHandler
{
    public record GetPhaseCostVarianceQuery(int projectPhaseId):IRequest<GeneralResponse<ProjectPhaseCostVarianceDto>>;
   
}
