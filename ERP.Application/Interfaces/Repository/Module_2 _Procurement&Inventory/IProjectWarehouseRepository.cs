using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory
{
    public interface IProjectWarehouseRepository : IGenericRepository<ProjectWarehouse>
    {
        Task<ProjectWarehouse?> GetByIdWithInclude(int projectwarehouseId, params Expression<Func<ProjectWarehouse, object>>[] Includes);
        Task<ProjectWarehouse?> GetByIdWithStockAsync(int warehouseId);
    }
}
