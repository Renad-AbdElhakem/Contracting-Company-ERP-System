using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.CommandHandler
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, GeneralResponse<Guid>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public CreateExpenseCommandHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public  async Task<GeneralResponse<Guid>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            if (request.expenseDto is null)
                return GeneralResponse<Guid>.Fail("Expense data is required.");

            var expense = _mapper.Map<Expense>(request.expenseDto);

            await _expenseRepository.AddAsync(expense);

            return GeneralResponse<Guid>.Success(expense.Id);
        }
    }
}
