using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery
{
    public interface IEquipmentRepository : IGenericRepository<Equipment>
    {
        Task<Equipment?> GetByIdWithInclude(Guid equipmentId, params Expression<Func<Equipment, object>>[] Includes);
        Task<List<Equipment>?> GetAllEquipmentWithCondition(Expression<Func<Equipment, bool>> Condition);
        //Task<IQueryable<Equipment>?> GetAllEquipmentWithCondition(Expression<Func<Equipment, bool>> Condition);
        IQueryable<Equipment> GetEquipmentsByFilterAsync();
        Task<List<Equipment>> GetAllEquipmentByIds(List<Guid> equipmentIds);
        Task UpdateRangeAsync(List<Equipment> equipments);
    }
}
