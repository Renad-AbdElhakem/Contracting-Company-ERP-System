using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Infrastructure.Repository.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectEmployeeRepository _projectEmployeeRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;

        public ProjectService(IProjectRepository projectRepository, IClientService clientService
                                , IMapper mapper, IEmployeeService employeeService
                                , IProjectEmployeeRepository projectEmployeeRepository, IProjectPhaseRepository phaseRepository)
        {
            _projectRepository = projectRepository;
            _clientService = clientService;
            _mapper = mapper;
            _employeeService = employeeService;
            _projectEmployeeRepository = projectEmployeeRepository;
            _projectPhaseRepository = phaseRepository;
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
            var projectPhase = await _projectRepository.GetByIdWithInclude(projectId, ph => ph.ProjectPhases);

            if (projectPhase is null)
                return GeneralResponse<List<ProjectPhasesDetailsDto>>.Fail($"Project with id {projectId} not found");

            var projectWithPhasesDetailsDto = _mapper.Map<List<ProjectPhasesDetailsDto>>(projectPhase.ProjectPhases);

            return GeneralResponse<List<ProjectPhasesDetailsDto>>.Success(projectWithPhasesDetailsDto);

        }

        public async Task<GeneralResponse<ProjectContractDto>> GetProjectContractDetails(Guid projectId)
        {
            var projectContractDetails = await _projectRepository.GetByIdWithInclude(projectId, c => c.ContractProject, x => x.Client);

            if (projectContractDetails is null)
                return GeneralResponse<ProjectContractDto>.Fail($"Project with id {projectId} not found");

            var projectWithContractDetailsDto = _mapper.Map<ProjectContractDto>(projectContractDetails);
            projectWithContractDetailsDto.ClientId = projectContractDetails.ClientId;
            projectWithContractDetailsDto.ClientName = projectContractDetails.Client.Name;

            return GeneralResponse<ProjectContractDto>.Success(projectWithContractDetailsDto);

        }

        public async Task<GeneralResponse<List<ProjectEmployeesDetailsDto>>> GetProjectEmployeesDetails(Guid projectId)
        {
            var projectEmployees = await _projectRepository.GetProjectWithEmployees(projectId);

            if (projectEmployees is null)
                return GeneralResponse<List<ProjectEmployeesDetailsDto>>.Fail($"Project with id {projectId} not found");

            var projectEmployeesDetailsDto = _mapper.Map<List<ProjectEmployeesDetailsDto>>(projectEmployees.ProjectEmployees);

            return GeneralResponse<List<ProjectEmployeesDetailsDto>>.Success(projectEmployeesDetailsDto);
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

            await _projectEmployeeRepository.AddAsync(projectEmployee);

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
                return GeneralResponse<int>.Fail(
                    $"Project with id {projectId} not found.");


            var existingProjectPhase = await _projectPhaseRepository.GetByProjectAndPhaseAsync(projectId, dto.PhaseId);

            if (existingProjectPhase is not null)
                return GeneralResponse<int>.Fail(
                    $"Phase with id {dto.PhaseId} is already assigned to project with id {projectId}.");

            var projectPhase = _mapper.Map<ProjectPhase>(dto);

            await _projectPhaseRepository.AddAsync(projectPhase);

            return GeneralResponse<int>.Success(projectPhase.Id);
        }

    }
}
