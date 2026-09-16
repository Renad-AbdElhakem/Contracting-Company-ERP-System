using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_4___Finance
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<List<Expense>> GetAllAsync(ExpenseFilterDto filter);

    }
}
