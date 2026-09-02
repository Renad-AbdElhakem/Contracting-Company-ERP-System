using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_3___Equipment_Machinery
{
    public class MaintenanceEmployeeRepository : GenericRepository<MaintenanceEmployee>, IMaintenanceEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MaintenanceEmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MaintenanceEmployee?> GetByMaintenanceAndEmployeeAsync(Guid maintenanceId, int employeeId)
        {
            return await _dbContext.MaintenanceEmployees
                .FirstOrDefaultAsync(me => me.Equipment_MaintenanceId == maintenanceId  && me.EmployeeId == employeeId);
        }
    }
}
