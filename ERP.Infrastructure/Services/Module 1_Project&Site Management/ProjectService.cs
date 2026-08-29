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
                                , IProjectEmployeeRepository projectEmployeeRepository,IProjectPhaseRepository phaseRepository)
        {
            _projectRepository = projectRepository;
            _clientService = clientService;
            _mapper = mapper;
            _employeeService = employeeService;
            _projectEmployeeRepository = projectEmployeeRepository;
            _projectPhaseRepository = phaseRepository;
        }


        public async Task<Guid?> CreateAsync(CreateProjectDto dto)
        {
            var client = await _clientService.GetByIdAsync(dto.ClientId);

            if (client is null)
                return null;

            var project = _mapper.Map<Project>(dto);

            await _projectRepository.AddAsync(project);

            return project.Id;
        }

        public async Task FinishProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project is null) return;

            project.Status = ProjectStatus.Completed;
            project.ActualEndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _projectRepository.UpdateAsync(project);
        }


        public async Task<ProjectWithPhasesDto?> GetProjectPhasesDetails(Guid projectId)
        {
            var projectPhase = await _projectRepository.GetByIdWithInclude(projectId, ph => ph.ProjectPhases);

            if (projectPhase is null)
                return null;

   


            var projectWithPhasesDetailsDto = new ProjectWithPhasesDto
            {
                ProjectId = projectId,
                ProjectPhasesDetailsDtos = projectPhase.ProjectPhases.Select(ph => new ProjectPhasesDetailsDto
                {
                    PhaseName = ph.Phase.Name,
                    StartDate = ph.StartDate,
                    DueDate = ph.DueDate,
                    FinishedDate = ph.FinishedDate,
                    ActualCost = ph.ActualCost,
                    EstimatedCost = ph.EstimatedCost,
                    ActualCostCalculatedAt = ph.ActualCostCalculatedAt,
                })
                .ToList()

            };

            return projectWithPhasesDetailsDto;

        }

        public async Task<ProjectContractDto?> GetProjectContractDetails(Guid projectId)
        {
            var projectContractDetails = await _projectRepository.GetByIdWithInclude(projectId, c => c.ContractProject);

            if (projectContractDetails is null)
                return null;

           

            var projectWithContractDetailsDto = new ProjectContractDto
            {
                ProjectId = projectId,
                ClientId = projectContractDetails.ClientId,
                ClientName = projectContractDetails.Client.Name,
                SignDate = projectContractDetails.ContractProject.SignDate,
                Status = projectContractDetails.ContractProject.Status,
                TotalAmount = projectContractDetails.ContractProject.TotalAmount,
                MaxPenaltyAmount = projectContractDetails.ContractProject.MaxPenaltyAmount,
                PenaltyClauseNote = projectContractDetails.ContractProject.PenaltyClauseNote,
                PenaltyPerDay = projectContractDetails.ContractProject.PenaltyPerDay,

            };

            return projectWithContractDetailsDto;

        }
        public async Task<List<ProjectEmployeesDetailsDto?>> GetProjectEmployeesDetails(Guid projectId)
        {
            var projectEmployees = await _projectRepository.GetProjectWithEmployees(projectId);

            if (projectEmployees is null)
                return null;

            var projectEmployeesDetailsDto = projectEmployees.ProjectEmployees.Select(e => new ProjectEmployeesDetailsDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.Employee.Name,
                Address = e.Employee.Address,
                Email = e.Employee.Email,
                Phone = e.Employee.Phone,
            }).ToList();


            return projectEmployeesDetailsDto;
        }


        public async Task<List<ProjectDetailsDto>> GetAllProjects()
        {
            var projectDetails = await _projectRepository.GetAllAsync();

            var projectDetailsDto = _mapper.Map<List<ProjectDetailsDto>>(projectDetails);

            return projectDetailsDto;

        }


        public async Task<bool> UpdateProjectTimeline(Guid projectId, UpdateProjectTimelineDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return false;

            if (dto.StartDate.HasValue)
                project.StartDate = dto.StartDate.Value;

            if (dto.ExpectedEndDate.HasValue)
                project.ExpectedEndDate = dto.ExpectedEndDate.Value;

            await _projectRepository.UpdateAsync(project);

            return true;
        }

        public async Task<bool> UpdateProject(Guid projectId, UpdateProjectDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);

            if (project is null)
                return false;

            if (dto.Name is not null)
                project.Name = dto.Name;

            if (dto.Description is not null)
                project.Description = dto.Description;

            if (dto.Status.HasValue)
                project.Status = dto.Status.Value;

            if (dto.Location is not null)
                project.Location = dto.Location;

            await _projectRepository.UpdateAsync(project);

            return true;
        }

        public async Task<GeneralResponse<Guid>> AssignEmployeeToProject(AssignProjectEmployeeDto dto)
        {
            var employee = await _employeeService.GetByIdAsync(dto.EmployeeId);

            if (employee is null)
                return GeneralResponse<Guid>.Fail($"Employee with id {dto.EmployeeId} not found.");

            if (employee.TerminationDate is not null || employee.Status == EmployeeStatus.Terminated)
                return GeneralResponse<Guid>.Fail($"Employee with id {dto.EmployeeId} is already terminated.");

            if (employee.Status != EmployeeStatus.Active)
                return GeneralResponse<Guid>.Fail($"Employee with id {dto.EmployeeId} is not active.");

            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            if (project is null)
                return GeneralResponse<Guid>.Fail($"Project with id {dto.ProjectId} not found.");

            if (project.ActualEndDate is not null && project.Status == ProjectStatus.Completed)
                return GeneralResponse<Guid>.Fail("Project is already completed.");

            if (project.Status != ProjectStatus.Planned && project.Status != ProjectStatus.InProgress)
                return GeneralResponse<Guid>.Fail("Project is not active and cannot be modified.");

            var projectEmployee = _mapper.Map<ProjectEmployee>(dto);

            await _projectEmployeeRepository.AddAsync(projectEmployee);

            return GeneralResponse<Guid>.Success(projectEmployee.Id);
        }


        public async Task<GeneralResponse<bool>> RemoveEmployeeFromProject(RemoveEmployeeFromProjectDto dto)
        {
            var projectEmployee = await _projectEmployeeRepository.GetByProjectAndEmployeeAsync(dto.ProjectId, dto.EmployeeId);

            if (projectEmployee is null)
                return GeneralResponse<bool>.Fail($"Employee with id {dto.EmployeeId} is not assigned to project with id {dto.ProjectId}.");

            await _projectEmployeeRepository.DeleteAsync(projectEmployee);

            return GeneralResponse<bool>.Success(true);
        }
        public async Task<GeneralResponse<int>> AddPhaseToProject(AssignProjectPhaseDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(dto.ProjectId);

            if (project is null)
                return GeneralResponse<int>.Fail(
                    $"Project with id {dto.ProjectId} not found.");


            var existingProjectPhase =await _projectPhaseRepository.GetByProjectAndPhaseAsync( dto.ProjectId,  dto.PhaseId);

            if (existingProjectPhase is not null)
                return GeneralResponse<int>.Fail(
                    $"Phase with id {dto.PhaseId} is already assigned to project with id {dto.ProjectId}.");

            var projectPhase = _mapper.Map<ProjectPhase>(dto);

            await _projectPhaseRepository.AddAsync(projectPhase);

            return GeneralResponse<int>.Success(projectPhase.Id);
        }
     
    }
}
