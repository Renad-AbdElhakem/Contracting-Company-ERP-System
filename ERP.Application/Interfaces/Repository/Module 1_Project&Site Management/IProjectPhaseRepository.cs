using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository
{
    public interface IProjectPhaseRepository : IGenericRepository<ProjectPhase>
    {
        Task<ProjectPhase?> GetByProjectAndPhaseAsync(Guid projectId, int phaseId);
        Task<ProjectPhase?> GetByIdWithInclude(int projectPhaseId, params Expression<Func<ProjectPhase, object>>[] Includes);
        Task<bool> IsExistAsync(int projectPhaseId);
        Task<ProjectPhase?> GetAllMaterialConsumptionsByProjectPhaseIdAsync(int projectPhaseId);
        Task<decimal> GetSumTotalExpenseByProjectPhaseId(int projectPhaseId);
        Task<ProjectPhase?> GetMaterialConsumptionByProjectPhaseId(int projectPhaseId);
        Task<decimal> GetTotalExpensesByProjectIdAsync(Guid projectId);
        Task<List<MaterialConsumption>> GetMaterialConsumptionsByProjectIdAsync(Guid projectId);
        Task<ProjectPhase?> GetAllMaterialVarianceByProjectPhaseIdAsync(int projectPhaseId);
    }
}
