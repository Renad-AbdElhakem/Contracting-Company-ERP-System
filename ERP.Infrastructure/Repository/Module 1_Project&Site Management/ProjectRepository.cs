using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }




        public async Task<Project?> GetByIdWithInclude(Guid projectId, params Expression<Func<Project, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

          return  await query.FirstOrDefaultAsync(p => p.Id == projectId);
        }


        public async Task<Project?> GetProjectWithPhase(Guid projectId)
        {
            return await _dbSet
                .Include(p => p.ProjectPhases)
                .ThenInclude(pe => pe.Phase)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }
        public async Task<Project?> GetProjectWithEmployees(Guid projectId)
        {
            return await _dbSet
                .Include(p => p.ProjectEmployees)
                .ThenInclude(pe => pe.Employee)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

       

        public async Task<Project?> GetProjectWithOrderRequestsAsync(Guid projectId)
        {
            return await _dbSet
                .Include(x => x.ProjectOrderRequests)
                    .ThenInclude(x => x.OrderMaterials)
                        .ThenInclude(x => x.Material)
                .FirstOrDefaultAsync(x => x.Id == projectId);
        }
    }
}
