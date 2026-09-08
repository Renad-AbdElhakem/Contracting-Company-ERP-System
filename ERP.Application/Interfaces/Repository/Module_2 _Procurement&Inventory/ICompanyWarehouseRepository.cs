using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory
{
    public interface ICompanyWarehouseRepository : IGenericRepository<CompanyWarehouse>
    {
        Task<CompanyWarehouse?> GetByIdWithInclude(int companywarehouseId, params Expression<Func<CompanyWarehouse, object>>[] Includes);
        Task<CompanyWarehouse?> GetByIdWithStockAsync(int warehouseId);


    }
}
