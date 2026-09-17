using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Domain.Model.Module_4___Finance;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_4___Finance
{
    public class ProjectFinancialSnapshotRepository : GenericRepository<ProjectFinancialSnapshot>, IProjectFinancialSnapshotRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectFinancialSnapshotRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<ProjectFinancialSnapshot>> GetFinancialSnapshotsByProjectId(Guid projectId)
        {
            return await _dbSet.Where(p => p.ProjectId == projectId).ToListAsync();

        }
        public async Task<ProjectFinancialSnapshot?> GetLastestFinancialSnapshotsByProjectId(Guid projectId)
        {
            return await _dbSet.Where(p => p.ProjectId == projectId).LastOrDefaultAsync();

        }
    }
}
