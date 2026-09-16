using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class ProjectPhaseRepository : GenericRepository<ProjectPhase>, IProjectPhaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectPhaseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProjectPhase?> GetByProjectAndPhaseAsync(Guid projectId, int phaseId)
        {
            return await _dbContext.ProjectPhases.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.PhaseId == phaseId);
        }

        public async Task<ProjectPhase?> GetByIdWithInclude(int projectPhaseId, params Expression<Func<ProjectPhase, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == projectPhaseId);
        }


        public async Task<bool> IsExistAsync(int projectPhaseId)
        {
            return await _dbSet.AnyAsync(p => p.Id == projectPhaseId);
        }


        public async Task<ProjectPhase?> GetAllMaterialConsumptionsByProjectPhaseIdAsync(int projectPhaseId)
        {

            return await _dbSet.Include(c => c.MaterialConsumptions)
                               .ThenInclude(m => m.Material)
                               .FirstOrDefaultAsync(ph => ph.Id == projectPhaseId);


        }


        public async Task<decimal> GetSumTotalExpenseByProjectPhaseId(int projectPhaseId)
        {
            return await _dbSet
          .Where(ph => ph.Id == projectPhaseId)
          .SelectMany(ph => ph.ProjectExpenses)
          .SumAsync(p => p.Amount);
        }

        public async Task<ProjectPhase?> GetMaterialConsumptionByProjectPhaseId(int projectPhaseId)
        {
            return await _dbSet.Include(m => m.MaterialConsumptions)
                     .FirstOrDefaultAsync(p => p.Id == projectPhaseId);
        }


        //R

        //public async Task<ProjectPhase?> GetAllPhaseMaterialVarianceByProjectPhaseIdAsync(int projectPhaseId)
        //{
        // return await _dbSet.Include(c => c.MaterialConsumptions)
        //                       .Include(r => r.ProjectMaterials)
        //                       .ThenInclude(m => m.Material)
        //                       .FirstOrDefaultAsync(ph => ph.Id == projectPhaseId);
        //}



    }
}
