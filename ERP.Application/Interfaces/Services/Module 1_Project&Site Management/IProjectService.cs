using ERP.Application.Dtos.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
    public interface IProjectService
    {
        Task<Guid?> CreateAsync(CreateProjectDto dto);

        Task FinishProjectAsync(Guid id);

        Task<ProjectWithPhasesDto?> GetProjectPhasesDetails(Guid projectId);

        Task<ProjectContractDto?> GetProjectContractDetails(Guid projectId);

        Task<List<ProjectEmployeesDetailsDto?>> GetProjectEmployeesDetails(Guid projectId);

        Task<List<ProjectDetailsDto>> GetAllProjects();

        Task<bool> UpdateProjectTimeline(Guid projectId, UpdateProjectTimelineDto dto);

        Task<bool> UpdateProject(Guid projectId, UpdateProjectDto dto);

        Task<GeneralResponse<Guid>> AssignEmployeeToProject(AssignProjectEmployeeDto dto);

        Task<GeneralResponse<bool>> RemoveEmployeeFromProject(RemoveEmployeeFromProjectDto dto);

        Task<GeneralResponse<int>> AddPhaseToProject(AssignProjectPhaseDto dto);
    }
}
