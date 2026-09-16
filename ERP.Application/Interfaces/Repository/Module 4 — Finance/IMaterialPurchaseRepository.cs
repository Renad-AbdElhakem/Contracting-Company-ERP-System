using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_4___Finance
{
    public interface IMaterialPurchaseRepository:IGenericRepository<MaterialPurchase>
    {
        Task<MaterialPurchase?> GetByIdWithInclude(Guid MaterialPurchaseId, params Expression<Func<MaterialPurchase, object>>[] Includes);

        Task<MaterialPurchase?> GetMaterialPurchaseDetailsById(Guid MaterialPurchaseId);
        Task<List<MaterialPurchaseItem>> GetMaterialPurchaseItemsByMaterialIds(List<Guid> materialIds);
    }
}
