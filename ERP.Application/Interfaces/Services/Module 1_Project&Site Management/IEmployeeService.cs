using ERP.Application.Dtos.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<int> CreateAsync(CreateEmployeeDto dto);

        Task<EmployeeDto?> GetByIdAsync(int id);

        Task<List<EmployeeDto>> GetAllAsync( string? role,  string? department);

        Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto);

        Task<bool> InactiveEmployee(int id);
    }
}
