using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery
{
    public interface IMaintenanceRepository:IGenericRepository<Equipment_Maintenance>
    {
        Task<Equipment_Maintenance?> GetByIdWithInclude(Guid maintenanceId, params Expression<Func<Equipment_Maintenance, object>>[] Includes);
        Task<Equipment_Maintenance?> GetMaintenanceEmployeesAsync(Guid maintenanceId);



    }
}
