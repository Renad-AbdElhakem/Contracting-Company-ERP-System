using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Command;
using ERP.Domain.Model.Module_4___Finance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.CommandHandler
{
    public class CreateProjectExpenseCommandHandler
     : IRequestHandler<CreateProjectExpenseCommand, GeneralResponse<Guid>>
    {
        private readonly IProjectPhasesService _phaseService;
        private readonly IMapper _mapper;

        public CreateProjectExpenseCommandHandler(IProjectPhasesService phaseService,IMapper mapper)
        {
            _phaseService = phaseService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<Guid>> Handle(CreateProjectExpenseCommand request, CancellationToken cancellationToken)
        {
            var result = await _phaseService.GetProjectPhaseWithExpensesAsync(
                request.ProjectPhaseId);

            if (!result.IsSuccess)
                return GeneralResponse<Guid>.Fail(result.Message);

            var projectPhase = result.Data;

            var projectExpense = _mapper.Map<ProjectExpense>(
                request.ExpenseDto);

            projectPhase.ProjectExpenses.Add(projectExpense);

            await _phaseService.UpdateAsync(projectPhase);

            return GeneralResponse<Guid>.Success(projectExpense.Id);
        }
    }
}
