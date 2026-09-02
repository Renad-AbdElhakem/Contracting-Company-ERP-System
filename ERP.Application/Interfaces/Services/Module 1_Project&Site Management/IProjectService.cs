using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
    public interface IProjectService
    {
        Task<GeneralResponse<Guid>> CreateAsync(CreateProjectDto dto);
        Task<GeneralResponse<bool>> FinishProjectAsync(Guid projectId);
        Task<GeneralResponse<bool>> CancelledProjectAsync(Guid projectId);
        Task<GeneralResponse<List<ProjectPhasesDetailsDto>>> GetProjectPhasesDetails(Guid projectId);

        Task<GeneralResponse<ProjectContractDto>> GetProjectContractDetails(Guid projectId);

        Task<GeneralResponse<List<EmployeeSummaryDto>>> GetProjectEmployeesDetails(Guid projectId);

        Task<List<ProjectDetailsDto>> GetAllProjects();
        Task<GeneralResponse<ProjectDetailsDto>> GetProjectById(Guid projectId);

        Task<GeneralResponse<bool>> UpdateProjectTimeline(Guid projectId, UpdateProjectTimelineDto dto);

        Task<GeneralResponse<bool>> UpdateProject(Guid projectId, UpdateProjectDto dto);

        Task<GeneralResponse<Guid>> AssignEmployeeToProject(Guid projectId, AssignEmployeeToProjectDto dto);

        Task<GeneralResponse<bool>> RemoveEmployeeFromProject(Guid projectId, int employeeId);

        Task<GeneralResponse<int>> AddPhaseToProject(Guid projectId, AssignProjectPhaseDto dto);

        Task<GeneralResponse<int>> AssignEquipmentToProject(Guid projectId, AssignProjectEquipment dto);
        Task<GeneralResponse<List<ProjectEquipmentDto>>> GetAllEquipmentByProjectId(Guid projectId);


    }
}
