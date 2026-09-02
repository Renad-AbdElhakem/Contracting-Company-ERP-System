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
    public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EquipmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Equipment?> GetByIdWithInclude(Guid equipmentId, params Expression<Func<Equipment, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == equipmentId);
        }
        public async Task<List<Equipment>?> GetAllEquipmentWithCondition(Expression<Func<Equipment, bool>> Condition)
        {
            return await _dbContext.Equipment.Where(Condition).ToListAsync();
        }
        public IQueryable<Equipment> GetEquipmentsByFilterAsync()
        {
            return _dbContext.Equipment.Include(s => s.Supplier);
        }

        public async Task<List<Equipment>> GetAllEquipmentByIds(List<Guid> equipmentIds)
        {

            return await _dbContext.Equipment.Where(e => equipmentIds.Contains(e.Id)).ToListAsync();
        }

        public async Task UpdateRangeAsync(List<Equipment> equipments)
        {
            _dbContext.Equipment.UpdateRange(equipments);
            await _dbContext.SaveChangesAsync();
        }

    }
}
