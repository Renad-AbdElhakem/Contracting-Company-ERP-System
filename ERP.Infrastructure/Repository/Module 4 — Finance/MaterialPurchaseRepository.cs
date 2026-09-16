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
    public class MaterialPurchaseRepository : GenericRepository<MaterialPurchase>, IMaterialPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialPurchaseRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;
        }


        public async Task<MaterialPurchase?> GetByIdWithInclude(Guid materialPurchaseId, params Expression<Func<MaterialPurchase, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == materialPurchaseId);
        }
        public async Task<MaterialPurchase?> GetMaterialPurchaseDetailsById(Guid MaterialPurchaseId)
        {
          return await  _dbSet.Include(pt=>pt.MaterialPurchaseItems)
                              .Include(py=>py.Payments)
             .FirstOrDefaultAsync(p => p.Id == MaterialPurchaseId);
        }









    }
}
