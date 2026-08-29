using ERP.Application.Dtos.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
    public interface IDepartmentService
    {
        Task<int> CreateAsync(CreateDepartmentDto dto);
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<List<DepartmentDto>> GetAllAsync();
        Task<bool> UpdateAsync(int id, UpdateDepartmentDto dto);
        Task<bool> InactiveDepartment(int id);
    }
   
}
