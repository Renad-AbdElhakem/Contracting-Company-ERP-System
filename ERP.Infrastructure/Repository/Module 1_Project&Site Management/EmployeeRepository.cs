using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employee?> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(e => e.Role)
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<List<Employee>> GetAllByFilterAsync( string? role, string? department)
        {
            var query = _dbSet
                .Include(e => e.Role)
                .Include(e => e.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(role))
            {
                query = query.Where(e =>
                    e.Role.RoleName.Contains(role));
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                query = query.Where(e =>
                    e.Department.Name.Contains(department));
            }

            return await query.ToListAsync();
        }

    }
}
