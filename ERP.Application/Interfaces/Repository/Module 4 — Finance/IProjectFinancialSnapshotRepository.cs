using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_4___Finance
{
    public interface IProjectFinancialSnapshotRepository:IGenericRepository<ProjectFinancialSnapshot>
    {
        Task<List<ProjectFinancialSnapshot>> GetFinancialSnapshotsByProjectId(Guid projectId);
        Task<ProjectFinancialSnapshot?> GetLastestFinancialSnapshotsByProjectId(Guid projectId);
    }
}
