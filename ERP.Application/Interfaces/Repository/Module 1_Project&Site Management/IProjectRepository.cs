using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management
{
    public interface IProjectRepository :IGenericRepository<Project>
    {
        Task<Project?> GetByIdWithInclude(Guid projectId, params Expression<Func<Project, object>>[] Includes);
        Task<Project?> GetProjectWithEmployees(Guid projectId);
        Task<Project?> GetProjectWithOrderRequestsAsync(Guid projectId);

    }
}
