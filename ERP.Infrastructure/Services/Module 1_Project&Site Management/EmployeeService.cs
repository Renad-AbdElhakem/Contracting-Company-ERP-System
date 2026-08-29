using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateEmployeeDto dto)
        {

            var employee = _mapper.Map<Employee>(dto);

            await _employeeRepository.AddAsync(employee);

            return employee.Id;
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee =
                await _employeeRepository.GetByIdWithDetailsAsync(id);

            if (employee is null)
                return null;

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<List<EmployeeDto>> GetAllAsync(string? role, string? department)
        {
            var employees = await _employeeRepository
                .GetAllByFilterAsync(role, department);

            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
                return false;

            _mapper.Map(dto, employee);

            await _employeeRepository.UpdateAsync(employee);

            return true;
        }

        public async Task<bool> InactiveEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
                return false;

            employee.Status = EmployeeStatus.Terminated;
            employee.TerminationDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _employeeRepository.UpdateAsync(employee);

            return true;
        }
    }
}
