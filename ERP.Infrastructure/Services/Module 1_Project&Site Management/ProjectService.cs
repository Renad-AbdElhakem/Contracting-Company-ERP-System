using AutoMapper;
using Azure.Core;
using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using ERP.Infrastructure.Repository.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IPhaseService _phaseService;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IEquipmentService _equipmentService;
        private readonly IProjectEquipmentService _projectEquipmentService;

        public ProjectService(IProjectRepository projectRepository, IClientService clientService
                                , IMapper mapper, IPhaseService phaseService, IEmployeeService employeeService
                                , IProjectEmployeeRepository projectEmployeeRepository, IProjectPhaseRepository phaseRepository
                                , IEquipmentService equipmentService, IProjectEquipmentService projectEquipmentService)
        {
            _projectRepository = projectRepository;
            _clientService = clientService;
            _mapper = mapper;
            _phaseService = phaseService;
            _employeeService = employeeService;
            _projectEmployeeRepository = projectEmployeeRepository;
            _projectPhaseRepository = phaseRepository;
            _equipmentService = equipmentService;
            _projectEquipmentService = projectEquipmentService;
        }


        public async Task<GeneralResponse<Guid>> CreateAsync(CreateProjectDto dto)
        {
            var client = await _clientService.GetByIdAsync(dto.ClientId);

            if (client is null)
                return GeneralResponse<Guid>.Fail("Client not found");

            var project = _mapper.Map<Project>(dto);

            await _projectRepository.AddAsync(project);

            return GeneralResponse<Guid>.Success(project.Id);
        }

        public async Task<GeneralResponse<bool>> FinishProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return GeneralResponse<bool>.Fail($"Project with id {projectId} not found");

            var response = await _projectEquipmentService.UnassignEquipmentAsync(projectId);

            if (!response.IsSuccess)
                return GeneralResponse<bool>.Fail(response.Message);

            project.Status = ProjectStatus.Completed;
            project.ActualEndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<bool>.Success(true);

        }

        public async Task<GeneralResponse<bool>> CancelledProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project is null)
                return GeneralResponse<bool>.Fail($"Project with id {projectId} not found");

            project.Status = ProjectStatus.Cancelled;
            project.ActualEndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<bool>.Success(true);

        }

        public async Task<GeneralResponse<List<ProjectPhasesDetailsDto>>> GetProjectPhasesDetails(Guid projectId)
        {
          //  var projectPhase = await _projectRepository.GetByIdWithInclude(projectId, ph => ph.ProjectPhases);
            var projectPhase = await _projectRepository.GetProjectWithPhase(projectId);

            if (projectPhase is null)
                return GeneralResponse<List<ProjectPhasesDetailsDto>>.Fail($"Project with id {projectId} not found");

            var projectWithPhasesDetailsDto = _mapper.Map<List<ProjectPhasesDetailsDto>>(projectPhase.ProjectPhases);

            return GeneralResponse<List<ProjectPhasesDetailsDto>>.Success(projectWithPhasesDetailsDto);

        }

      

        public async Task<GeneralResponse<List<EmployeeSummaryDto>>> GetProjectEmployeesDetails(Guid projectId)
        {
            var projectEmployees = await _projectRepository.GetProjectWithEmployees(projectId);

            if (projectEmployees is null)
                return GeneralResponse<List<EmployeeSummaryDto>>.Fail($"Project with id {projectId} not found");

            var projectEmployeesDetailsDto = _mapper.Map<List<EmployeeSummaryDto>>(projectEmployees.ProjectEmployees);

            return GeneralResponse<List<EmployeeSummaryDto>>.Success(projectEmployeesDetailsDto);
        }


        public async Task<List<ProjectDetailsDto>> GetAllProjects()
        {
            var projectsDetails = await _projectRepository.GetAllAsync(c => c.Client);

            var projectsDetailsDto = _mapper.Map<List<ProjectDetailsDto>>(projectsDetails);

            return projectsDetailsDto;

        }
        public async Task<GeneralResponse<ProjectDetailsDto>> GetProjectById(Guid projectId)
        {
            var projectDetails = await _projectRepository.GetByIdWithInclude(projectId, c => c.Client);
            if (projectDetails is null)
                return GeneralResponse<ProjectDetailsDto>.Fail($"Project with id {projectId} not found");
            var projectDetailsDto = _mapper.Map<ProjectDetailsDto>(projectDetails);

            return GeneralResponse<ProjectDetailsDto>.Success(projectDetailsDto);

        }


        public async Task<GeneralResponse<bool>> UpdateProjectTimeline(Guid projectId, UpdateProjectTimelineDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return GeneralResponse<bool>.Fail($"Project with id {projectId} not found");

            if (dto.StartDate.HasValue)
                project.StartDate = dto.StartDate.Value;

            if (dto.ExpectedEndDate.HasValue)
                project.ExpectedEndDate = dto.ExpectedEndDate.Value;

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<bool>> UpdateProject(Guid projectId, UpdateProjectDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return GeneralResponse<bool>.Fail($"Project with id {projectId} not found");

            if (dto.Name is not null)
                project.Name = dto.Name;

            if (dto.Description is not null)
                project.Description = dto.Description;

            if (dto.Status.HasValue)
                project.Status = dto.Status.Value;

            if (dto.Location is not null)
                project.Location = dto.Location;

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<Guid>> AssignEmployeeToProject(Guid projectId, AssignEmployeeToProjectDto dto)
        {
            var employee = await _employeeService.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                return GeneralResponse<Guid>.Fail($"Employee with id {dto.EmployeeId} not found.");

            if (employee.TerminationDate is not null || employee.Status == EmployeeStatus.Terminated)
                return GeneralResponse<Guid>.Fail($"Employee with id {dto.EmployeeId} is already terminated.");

            if (employee.Status != EmployeeStatus.Active)
                return GeneralResponse<Guid>.Fail($"Employee with id {dto.EmployeeId} is not active.");

            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return GeneralResponse<Guid>.Fail($"Project with id {projectId} not found.");

            if (project.ActualEndDate is not null && project.Status == ProjectStatus.Completed)
                return GeneralResponse<Guid>.Fail("Project is already completed.");

            if (project.Status != ProjectStatus.Planned && project.Status != ProjectStatus.InProgress)
                return GeneralResponse<Guid>.Fail("Project is not active and cannot be modified.");

            var projectEmployee = _mapper.Map<ProjectEmployee>(dto);
            projectEmployee.ProjectId = projectId;

            project.ProjectEmployees.Add(projectEmployee);

            await _projectRepository.UpdateAsync(project);

            // await _projectEmployeeRepository.AddAsync(projectEmployee);

            return GeneralResponse<Guid>.Success(projectEmployee.Id);
        }


        public async Task<GeneralResponse<bool>> RemoveEmployeeFromProject(Guid projectId, int employeeId)
        {
            var projectEmployee = await _projectEmployeeRepository.GetByProjectAndEmployeeAsync(projectId, employeeId);

            if (projectEmployee is null)
                return GeneralResponse<bool>.Fail($"Employee with id {employeeId} is not assigned to project with id {projectId}.");

            await _projectEmployeeRepository.DeleteAsync(projectEmployee);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<int>> AddPhaseToProject(Guid projectId, AssignProjectPhaseDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return GeneralResponse<int>.Fail($"Project with id {projectId} not found.");

            var existingPhase = await _phaseService.GetPhaseByIdAsync(dto.PhaseId);

            if (!existingPhase.IsSuccess)
                return GeneralResponse<int>.Fail($"Phase with id {dto.PhaseId} not found.");

            var existingProjectPhase = await _projectPhaseRepository.GetByProjectAndPhaseAsync(projectId, dto.PhaseId);

            if (existingProjectPhase is not null)
                return GeneralResponse<int>.Fail($"Phase with id {dto.PhaseId} is already assigned to project with id {projectId}.");

            var projectPhase = _mapper.Map<ProjectPhase>(dto);
            projectPhase.ProjectId = projectId;
            await _projectPhaseRepository.AddAsync(projectPhase);

            return GeneralResponse<int>.Success(projectPhase.Id);
        }

        public async Task<GeneralResponse<int>> AssignEquipmentToProject(Guid projectId, AssignProjectEquipment dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return GeneralResponse<int>.Fail($"Project with id {projectId} not found.");

            if (project.ActualEndDate is not null)
                return GeneralResponse<int>.Fail($"Project with id {projectId} already finished.");


            var equipment = await _equipmentService.GetEquipmentById(dto.EquipmentId);

            if (!equipment.IsSuccess)
                return GeneralResponse<int>.Fail(equipment.Message);

            if (equipment.Data.EquipmentStatus != EquipmentStatus.Available)
                return GeneralResponse<int>.Fail($"Equipment is not available. Current status: {equipment.Data.EquipmentStatus}.");

            var result = await _equipmentService.MarkAsInUseAsync(equipment.Data.Id);
            if (!result.IsSuccess)
                return GeneralResponse<int>.Fail(result.Message);

            var projectEquipment = _mapper.Map<ProjectEquipment>(dto);
            projectEquipment.ProjectId = projectId;

            project.ProjectEquipment.Add(projectEquipment);

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<int>.Success(projectEquipment.Id);

        }

        public async Task<GeneralResponse<List<ProjectEquipmentDto>>> GetAllEquipmentByProjectId(Guid projectId)
        {
            var project = await _projectRepository.GetByIdWithInclude(projectId, e => e.ProjectEquipment);

            if (project is null)
                return GeneralResponse<List<ProjectEquipmentDto>>.Fail($"Project with id {projectId} not found");

            var projectEquipmentDto = _mapper.Map<List<ProjectEquipmentDto>>(project.ProjectEquipment);

            return GeneralResponse<List<ProjectEquipmentDto>>.Success(projectEquipmentDto);

        }

        public async Task<GeneralResponse<int>> CreateProjectOrderRequestAsync(Guid projectId, CreateProjectOrderRequestDto orderRequestDto)
        {
            if (orderRequestDto is null || !orderRequestDto.orderMaterialsDtos.Any())
                return GeneralResponse<int>.Fail("Order request must contain at least one material");

            var project = await _projectRepository.GetByIdWithInclude(projectId, e => e.ProjectOrderRequests);

            if (project == null)
                return GeneralResponse<int>.Fail($"Project with id {projectId} not found");

            if (project.ActualEndDate != null)
                return GeneralResponse<int>.Fail($"Project with id {projectId} has already been completed");

            if (project.Status is ProjectStatus.OnHold or ProjectStatus.Completed or ProjectStatus.Closed or ProjectStatus.Cancelled)
                return GeneralResponse<int>.Fail($"Cannot create order for project with status {project.Status}");

            var employee = await _employeeService.GetByIdAsync(orderRequestDto.RequestByEmployeeId);

            if (employee == null)
                return GeneralResponse<int>.Fail($"Employee with id {orderRequestDto.RequestByEmployeeId} not found");

            if (employee.TerminationDate != null)
                return GeneralResponse<int>.Fail($"Employee with id {orderRequestDto.RequestByEmployeeId} is terminated");

            if (employee.Status != EmployeeStatus.Active)
                return GeneralResponse<int>.Fail($"Employee is {employee.Status}"); ;


            var orderRequest = _mapper.Map<ProjectOrderRequest>(orderRequestDto);

            orderRequest.RequestDate = DateOnly.FromDateTime(DateTime.Now);
            orderRequest.Status = RequestStatus.Pending;
            orderRequest.ProjectId = projectId;

            var materialsOrdered = _mapper.Map<List<OrderMaterials>>(orderRequestDto.orderMaterialsDtos);

            orderRequest.OrderMaterials = materialsOrdered;

            project.ProjectOrderRequests.Add(orderRequest);

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<int>.Success(orderRequest.Id, "Order request created ");
        }

        public async Task<GeneralResponse<Project>> GetProjectWithInclude(Guid projectId, params Expression<Func<Project, object>>[] Includes)
        {
            var project = await _projectRepository.GetByIdWithInclude(projectId, Includes);
            return GeneralResponse<Project>.Success(project);
        }

        public async Task UpdateAsync(Project project)
        {
            await _projectRepository.UpdateAsync(project);
        }
    }
}
