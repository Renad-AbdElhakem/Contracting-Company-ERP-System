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
    public class ProjectPhaseRepository : GenericRepository<ProjectPhase>, IProjectPhaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectPhaseRepository(ApplicationDbContext dbContext):base(dbContext)
        {
           _dbContext = dbContext;
        }
        public async Task<ProjectPhase?> GetByProjectAndPhaseAsync( Guid projectId, int phaseId)
        {
            return await _dbContext.ProjectPhases.FirstOrDefaultAsync(x =>  x.ProjectId == projectId && x.PhaseId == phaseId);
        }
    }
}
