using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_4___Finance
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Expense>> GetAllAsync(ExpenseFilterDto filter)
        {
            var query = _dbSet.AsQueryable();

            if (filter.Type.HasValue)
                query = query.Where(x => x.Type == filter.Type.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(x => x.Date >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(x => x.Date <= filter.ToDate.Value);

            return await query.ToListAsync();
        }

        

    }
}
