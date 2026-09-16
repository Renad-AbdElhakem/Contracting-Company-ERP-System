using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_4___Finance
{
    public interface IContractPaymentRecordRepository:IGenericRepository<ContractPaymentRecord>
    {
        Task AddRangeAsync(List<ContractPaymentRecord> record);
    }
}
