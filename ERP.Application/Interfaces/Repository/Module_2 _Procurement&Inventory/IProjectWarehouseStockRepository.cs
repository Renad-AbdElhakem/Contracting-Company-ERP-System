using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory
{
    public interface IProjectWarehouseStockRepository : IGenericRepository<ProjectWarehouseStock>
    {
        Task<ProjectWarehouseStock?> GetByIdWithInclude(int projectwarehouseStockId, params Expression<Func<ProjectWarehouseStock, object>>[] Includes);
        Task<ProjectWarehouseStock?> GetByConditionAsync(Expression<Func<ProjectWarehouseStock, bool>> condition);
        Task<bool> IsMaterialExistAtProjectwarehouseStock(Guid materialId);



    }
}
