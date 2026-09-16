using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_4___Finance;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_4___Finance
{
    public class ContracInstallmentPlanRepository : GenericRepository<ContractInstallmentPlan>, IContractInstallmentPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public ContracInstallmentPlanRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ContractInstallmentPlan?> GetByIdWithInclude(int ContractInstallmentPlanId, params Expression<Func<ContractInstallmentPlan, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == ContractInstallmentPlanId);
        }
    }
}
