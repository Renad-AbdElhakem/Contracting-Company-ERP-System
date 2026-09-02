using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
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
    public class ProjectEquipmentRepository:GenericRepository<ProjectEquipment>, IProjectEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectEquipmentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProjectEquipment>> GetAllWithConditionAsync(Expression<Func<ProjectEquipment, bool>> Condition)
        {
            return  await _dbContext.ProjectEquipment.Where(Condition).ToListAsync();
        }
        public async Task<ProjectEquipment> GetWithConditionAsync(Expression<Func<ProjectEquipment, bool>> Condition)
        {
            return  await _dbContext.ProjectEquipment.FirstOrDefaultAsync(Condition);
        }
        public async Task UpdateRangeAsync(List<ProjectEquipment> projectEquipments)
        {
            _dbContext.ProjectEquipment.UpdateRange(projectEquipments);
            await _dbContext.SaveChangesAsync();
        }
    }
}
