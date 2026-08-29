using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management
{
    public interface IEmployeeRepository :IGenericRepository<Employee>
    {
        Task<Employee?> GetByIdWithDetailsAsync(int id);
        Task<List<Employee>> GetAllByFilterAsync(string? role, string? department);
    }
}
