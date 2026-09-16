using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Domain.Model;
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
    public class ContractProjectRepository : GenericRepository<ContractProject>, IContractProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ContractProject?> GetByIdWithInclude(Guid contractProjectId, params Expression<Func<ContractProject, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == contractProjectId);
        }
        public async Task<int> GetLatePaymentsCountAsync(Guid contractProjectId)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            return await _dbSet
                       .Where(cp => cp.Id == contractProjectId)
                       .SelectMany(pr => pr.ContractPaymentRecords
                       .Where(r => r.AmountPaid == null && r.DueDate < today))
                       .CountAsync();

            
        }
    }
}
