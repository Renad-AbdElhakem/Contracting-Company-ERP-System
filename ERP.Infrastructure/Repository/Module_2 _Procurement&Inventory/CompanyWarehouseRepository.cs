using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_2__Procurement_Inventory
{
    public class CompanyWarehouseRepository : GenericRepository<CompanyWarehouse>, ICompanyWarehouseRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyWarehouseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<CompanyWarehouse?> GetByIdWithInclude(int companywarehouseId, params Expression<Func<CompanyWarehouse, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == companywarehouseId);
        }
        public async Task<CompanyWarehouse?> GetByIdWithStockAsync(int warehouseId)
        {
            return await _dbSet
                .Include(w => w.CompanyWarehouseStocks)
                .ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(w => w.Id == warehouseId);
        }
    }
}
