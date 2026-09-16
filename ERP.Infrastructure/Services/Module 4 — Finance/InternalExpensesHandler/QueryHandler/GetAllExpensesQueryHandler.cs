using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.QueryHandler
{
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, GeneralResponse<List<ExpenseDto>>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetAllExpensesQueryHandler( IExpenseRepository expenseRepository,  IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<ExpenseDto>>> Handle(GetAllExpensesQuery request,CancellationToken cancellationToken)
        {
            if (request.Filter.FromDate.HasValue && request.Filter.ToDate.HasValue && request.Filter.FromDate > request.Filter.ToDate)
            {
                return GeneralResponse<List<ExpenseDto>>.Fail("From date cannot be greater than to date.");
            }

            var expenses = await _expenseRepository.GetAllAsync(request.Filter);

            var expenseDtos = _mapper.Map<List<ExpenseDto>>(expenses);

            return GeneralResponse<List<ExpenseDto>>.Success(expenseDtos);
        }
    }
}
