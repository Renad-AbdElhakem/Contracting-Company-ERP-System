using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using ERP.Infrastructure.Persistence;
using ERP.Infrastructure.Repository.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_3___Equipment_Machinery
{
    public class MaintenanceService : IMaintenanceService
    {

        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IMaintenanceEmployeeRepository _maintenanceEmployeeRepository;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository, IMapper mapper
                                   , IEmployeeService employeeService, IMaintenanceEmployeeRepository maintenanceEmployeeRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _mapper = mapper;
            _employeeService = employeeService;
            _maintenanceEmployeeRepository = maintenanceEmployeeRepository;
        }

        public async Task<GeneralResponse<List<EmployeeSummaryDto>>> GetEmployeesAtMaintenanceByIdAsync(Guid maintenanceId)
        {
            var maintenance = await _maintenanceRepository.GetMaintenanceEmployeesAsync(maintenanceId);

            if (maintenance == null)
                return GeneralResponse<List<EmployeeSummaryDto>>.Fail($"Maintenance with id {maintenanceId} not found");

            var employees = maintenance.MaintenanceEmployees.Select(e => e.Employee).ToList();

            var employeeSummaryDetails = _mapper.Map<List<EmployeeSummaryDto>>(employees);

            return GeneralResponse<List<EmployeeSummaryDto>>.Success(employeeSummaryDetails);
        }

        public async Task<GeneralResponse<int>> AssignEmployeeToMaintenanceAsync(Guid maintenanceId, AssignEmployeeToMaintenanceDto dto)
        {
            var maintenance = await _maintenanceRepository.GetByIdAsync(maintenanceId);

            if (maintenance == null)
                return GeneralResponse<int>.Fail($"Maintenance with id {maintenanceId} not found");

            if (maintenance.EndDate is not null)
                return GeneralResponse<int>.Fail($"Maintenance with id {maintenanceId} already finished.");

            var employee = await _employeeService.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                return GeneralResponse<int>.Fail($"Employee with id {dto.EmployeeId} not found.");

            if (employee.TerminationDate is not null || employee.Status == EmployeeStatus.Terminated)
                return GeneralResponse<int>.Fail($"Employee with id {dto.EmployeeId} is already terminated.");

            if (employee.Status != EmployeeStatus.Active)
                return GeneralResponse<int>.Fail($"Employee with id {dto.EmployeeId} is not active.");

            var maintenanceEmployee = new MaintenanceEmployee
            {
                Equipment_MaintenanceId = maintenanceId,
                EmployeeId = dto.EmployeeId
            };

            await _maintenanceEmployeeRepository.AddAsync(maintenanceEmployee);

            return GeneralResponse<int>.Success(maintenanceEmployee.Id);
        }

        public async Task<GeneralResponse<bool>> RemoveEmployeeFromMaintenanceAsync(Guid maintenanceId, int employeeId)
        {
            var maintenanceEmployee = await _maintenanceEmployeeRepository.GetByMaintenanceAndEmployeeAsync(maintenanceId, employeeId);

            if (maintenanceEmployee is null)
                return GeneralResponse<bool>.Fail($"Employee with id {employeeId} is not assigned to maintenance with id {maintenanceId}.");

            await _maintenanceEmployeeRepository.DeleteAsync(maintenanceEmployee);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<Guid>> AddNewMaintenance(Guid equipmentId, AddMaintenanceDto dto)
        {
            if (dto is null)
                return GeneralResponse<Guid>.Fail("Maintenance data is required.");

            var maintence = _mapper.Map<Equipment_Maintenance>(dto);

            maintence.EquipmentId = equipmentId;

            await _maintenanceRepository.AddAsync(maintence);

            return GeneralResponse<Guid>.Success(maintence.Id);

        }
        public async Task<GeneralResponse<bool>> UpdateMaintenanceAsync(Guid maintenanceId, UpdateMaintenanceDto dto)
        {
            var maintenance = await _maintenanceRepository.GetByIdAsync(maintenanceId);

            if (maintenance == null)
                return GeneralResponse<bool>.Fail($"Maintenance with id {maintenanceId} not found");

            if (maintenance.StartDate < dto.StartDate)
                return GeneralResponse<bool>.Fail("Maintenance already started.");

            if (maintenance.EndDate is not null)
                return GeneralResponse<bool>.Fail("Maintenance already finished.");

            if (dto.StartDate.HasValue)
                maintenance.StartDate = dto.StartDate.Value;

            if (dto.Note is not null)
                maintenance.Note = dto.Note;

            await _maintenanceRepository.UpdateAsync(maintenance);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<Guid>> CompleteAsync(Guid maintenancetId, EndMaintenanceDto dto)
        {
            if (dto is null)
                return GeneralResponse<Guid>.Fail("Maintenance cost is required.");

            var maintenance = await _maintenanceRepository.GetByIdAsync(maintenancetId);

            if (maintenance is null)
                return GeneralResponse<Guid>.Fail($"Maintenance with id {maintenancetId} not found.");

            if (maintenance.EndDate is not null)
                return GeneralResponse<Guid>.Fail("Maintenance already finished.");

            maintenance.Cost = dto.Cost;
            maintenance.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _maintenanceRepository.UpdateAsync(maintenance);

            return GeneralResponse<Guid>.Success(maintenance.EquipmentId);

        }

    }
}
