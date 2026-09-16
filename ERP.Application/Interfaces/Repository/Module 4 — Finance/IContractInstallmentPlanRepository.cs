using ERP.Domain.Model;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_4___Finance
{
    public interface IContractInstallmentPlanRepository : IGenericRepository<ContractInstallmentPlan>
    {
        Task<ContractInstallmentPlan?> GetByIdWithInclude(int ContractPaymentRecordId, params Expression<Func<ContractInstallmentPlan, object>>[] Includes);
    }
}
