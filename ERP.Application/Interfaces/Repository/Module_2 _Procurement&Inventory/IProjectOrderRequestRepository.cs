using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory
{
    public interface IProjectOrderRequestRepository : IGenericRepository<ProjectOrderRequest>
    {
        Task<ProjectOrderRequest?> GetByIdWithInclude(int orderRequestId, params Expression<Func<ProjectOrderRequest, object>>[] Includes);
        Task<ProjectOrderRequest?> GetOrderRequestWithMaterialsByIdAsync(int orderRequestId);

    }
}
