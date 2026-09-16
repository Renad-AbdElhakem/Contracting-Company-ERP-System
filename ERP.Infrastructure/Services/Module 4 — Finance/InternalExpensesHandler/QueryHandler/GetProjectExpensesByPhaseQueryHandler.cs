using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.QueryHandler
{
    public class GetProjectExpensesByPhaseQueryHandler: IRequestHandler<GetProjectExpensesByPhaseQuery, GeneralResponse<List<ProjectExpenseDto>>>
    {
        private readonly IProjectPhasesService _projectPhasesService;
        private readonly IMapper _mapper;

        public GetProjectExpensesByPhaseQueryHandler(IProjectPhasesService projectPhasesService,IMapper mapper)
        {
            _projectPhasesService = projectPhasesService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<ProjectExpenseDto>>> Handle(GetProjectExpensesByPhaseQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectPhasesService.GetProjectPhaseWithExpensesAsync(request.ProjectPhaseId);

            if (!result.IsSuccess)
                return GeneralResponse<List<ProjectExpenseDto>>.Fail(result.Message);

            var projectExpenses = result.Data.ProjectExpenses;

            var projectExpenseDtos = _mapper.Map<List<ProjectExpenseDto>>(projectExpenses);

            return GeneralResponse<List<ProjectExpenseDto>>.Success(projectExpenseDtos);
        }
    }
}
