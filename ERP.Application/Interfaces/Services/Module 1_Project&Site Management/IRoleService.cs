using ERP.Application.Dtos.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
    public interface IRoleService
    {
        Task<int> CreateAsync(CreateRoleDto dto);
        Task<RoleDto?> GetByIdAsync(int id);
        Task<List<RoleDto>> GetAllAsync();
        Task<bool> UpdateAsync(int id, UpdateRoleDto dto);
       
    }
}
