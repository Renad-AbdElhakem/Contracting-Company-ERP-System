using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository
{
    public interface IProjectEmployeeRepository: IGenericRepository<ProjectEmployee>
    {
        Task<ProjectEmployee?> GetByProjectAndEmployeeAsync(Guid projectId, int employeeId);
    }
}
