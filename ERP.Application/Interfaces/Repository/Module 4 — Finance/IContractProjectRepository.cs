using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_4___Finance
{
    public interface IContractProjectRepository : IGenericRepository<ContractProject>
    {
        Task<ContractProject?> GetByIdWithInclude(Guid contractProjectId, params Expression<Func<ContractProject, object>>[] Includes);
        Task<int> GetLatePaymentsCountAsync(Guid contractProjectId);
    }
}
