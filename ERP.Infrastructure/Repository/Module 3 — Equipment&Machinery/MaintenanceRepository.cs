using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_3___Equipment_Machinery
{
    public class MaintenanceRepository : GenericRepository<Equipment_Maintenance>, IMaintenanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MaintenanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Equipment_Maintenance?> GetByIdWithInclude(Guid maintenanceId, params Expression<Func<Equipment_Maintenance, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == maintenanceId);
        }
        public async Task<Equipment_Maintenance?> GetMaintenanceEmployeesAsync(Guid maintenanceId)
        {
            return await _dbContext.Equipment_Maintenances.Include(e => e.MaintenanceEmployees)
                                                          .ThenInclude(e => e.Employee)
                                                          .FirstOrDefaultAsync(m => m.Id == maintenanceId);
        }
    }
}
