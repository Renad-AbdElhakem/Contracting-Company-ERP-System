using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateRoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);

            await _roleRepository.AddAsync(role);

            return role.Id;
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role =
                await _roleRepository.GetByIdAsync(id);

            if (role is null)
                return null;

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return _mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateRoleDto dto)
        {
            var role =
                await _roleRepository.GetByIdAsync(id);

            if (role is null)
                return false;

            _mapper.Map(dto, role);

            await _roleRepository.UpdateAsync(role);

            return true;
        }

       

    }
}
