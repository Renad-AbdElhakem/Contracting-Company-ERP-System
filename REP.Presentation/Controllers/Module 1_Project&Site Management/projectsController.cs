using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public projectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            var result = await _projectService.CreateAsync(dto);

            if (result is null)
                return BadRequest("Client not found.");

            return Ok(result);
        }


        [HttpPatch("{id}/finish")]
        public async Task<IActionResult> FinishProject(Guid id)
        {
            await _projectService.FinishProjectAsync(id);

            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectService.GetAllProjects();

            return Ok(result);
        }


        [HttpGet("{projectId}/phases")]
        public async Task<IActionResult> GetProjectPhases(Guid projectId)
        {
            var result = await _projectService.GetProjectPhasesDetails(projectId);

            if (result is null)
                return NotFound($"Project with id {projectId} not found.");

            return Ok(result);
        }


        [HttpGet("{projectId}/contract")]
        public async Task<IActionResult> GetProjectContract(Guid projectId)
        {
            var result = await _projectService.GetProjectContractDetails(projectId);

            if (result is null)
                return NotFound($"Project with id {projectId} not found.");

            return Ok(result);
        }


        [HttpGet("{projectId}/employees")]
        public async Task<IActionResult> GetProjectEmployees(Guid projectId)
        {
            var result = await _projectService.GetProjectEmployeesDetails(projectId);

            if (result is null)
                return NotFound($"Project with id {projectId} not found.");

            return Ok(result);
        }


        [HttpPatch("{projectId}/timeline")]
        public async Task<IActionResult> UpdateProjectTimeline(Guid projectId, UpdateProjectTimelineDto dto)
        {
            var result = await _projectService.UpdateProjectTimeline(projectId, dto);

            if (!result)
                return NotFound($"Project with id {projectId} not found.");

            return Ok(true);
        }


        [HttpPatch("{projectId}")]
        public async Task<IActionResult> UpdateProject(Guid projectId,UpdateProjectDto dto)
        {
            var result = await _projectService.UpdateProject(projectId, dto);

            if (!result)
                return NotFound($"Project with id {projectId} not found.");

            return Ok(true);
        }


        [HttpPost("employees")]
        public async Task<IActionResult> AssignEmployeeToProject(AssignProjectEmployeeDto dto)
        {
            var result = await _projectService.AssignEmployeeToProject(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpDelete("employees")]
        public async Task<IActionResult> RemoveEmployeeFromProject(RemoveEmployeeFromProjectDto dto)
        {
            var result = await _projectService.RemoveEmployeeFromProject(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("phases")]
        public async Task<IActionResult> AddPhaseToProject(AssignProjectPhaseDto dto)
        {
            var result = await _projectService.AddPhaseToProject(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
