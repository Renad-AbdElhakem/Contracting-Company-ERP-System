using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateDepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);

            await _departmentRepository.AddAsync(department);

            return department.Id;
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department =
                await _departmentRepository.GetByIdAsync(id);

            if (department is null)
                return null;

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments =
                await _departmentRepository.GetAllAsync();

            return _mapper.Map<List<DepartmentDto>>(departments);
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateDepartmentDto dto)
        {
            var department =
                await _departmentRepository.GetByIdAsync(id);

            if (department is null)
                return false;

            _mapper.Map(dto, department);

            await _departmentRepository.UpdateAsync(department);

            return true;
        }

        public async Task<bool> InactiveDepartment(int id)
        {
            var department =
                await _departmentRepository.GetByIdAsync(id);

            if (department is null)
                return false;

            department.IsAvailable = false;
            await _departmentRepository.UpdateAsync(department);

            return true;
        }
    }
}
