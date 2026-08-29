using ERP.Application.Interfaces.Repository;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class ProjectEmployeeRepository : GenericRepository<ProjectEmployee>, IProjectEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectEmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProjectEmployee?> GetByProjectAndEmployeeAsync(Guid projectId,int employeeId)
   
        {
            return await _dbContext.ProjectEmployees.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.EmployeeId == employeeId);
        }
    }
}
