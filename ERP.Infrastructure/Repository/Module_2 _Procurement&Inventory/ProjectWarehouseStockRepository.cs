using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
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
    public class ProjectWarehouseStockRepository : GenericRepository<ProjectWarehouseStock>, IProjectWarehouseStockRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectWarehouseStockRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ProjectWarehouseStock?> GetByIdWithInclude(int projectwarehouseStockId, params Expression<Func<ProjectWarehouseStock, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == projectwarehouseStockId);
        }
        public async Task<ProjectWarehouseStock?> GetByConditionAsync(Expression<Func<ProjectWarehouseStock, bool>> condition)
        {
            var query = _dbSet.AsQueryable();

            return await query.FirstOrDefaultAsync(condition);
        }
        public async Task<bool> IsMaterialExistAtProjectwarehouseStock(Guid materialId)
        {
            return await _dbSet.AnyAsync(s => s.MaterialId == materialId);
        }
    }

}
