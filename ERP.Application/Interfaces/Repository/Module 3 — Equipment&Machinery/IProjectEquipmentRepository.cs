using ERP.Domain.Model.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery
{
    public interface IProjectEquipmentRepository:IGenericRepository<ProjectEquipment>
    {
        Task<List<ProjectEquipment>> GetAllWithConditionAsync(Expression<Func<ProjectEquipment, bool>> Condition);
        Task<ProjectEquipment> GetWithConditionAsync(Expression<Func<ProjectEquipment, bool>> Condition);
        Task UpdateRangeAsync(List<ProjectEquipment> projectEquipments);
    }
}
