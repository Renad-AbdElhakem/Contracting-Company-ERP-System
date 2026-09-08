using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_2__Procurement_Inventory
{
    public class ProjectWarehouseRepository : GenericRepository<ProjectWarehouse>, IProjectWarehouseRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectWarehouseRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;
        }

        public async Task<ProjectWarehouse?> GetByIdWithInclude(int projectwarehouseId, params Expression<Func<ProjectWarehouse, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == projectwarehouseId);
        }
        public async Task<ProjectWarehouse?> GetByIdWithStockAsync(int warehouseId)
        {
            return await _dbSet
                .Include(w => w.ProjectWarehouseStocks)
                .ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(w => w.Id == warehouseId);
        }
    }
}
