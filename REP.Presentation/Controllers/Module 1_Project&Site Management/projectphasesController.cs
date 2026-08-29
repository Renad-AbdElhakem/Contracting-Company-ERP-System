using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model._1_Project_Site_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectphasesController : ControllerBase
    {
        private readonly IProjectPhasesService _projectPhasesService;

        public projectphasesController(IProjectPhasesService projectPhasesService)
        {
            _projectPhasesService = projectPhasesService;
        }


        [HttpPatch("{projectPhaseId}/dates")]
        public async Task<IActionResult> UpdateProjectPhaseDates(int projectPhaseId, UpdateProjectPhaseDatesDto dto)
        {
            var result = await _projectPhasesService.UpdateProjectPhaseDates(projectPhaseId, dto);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }


        [HttpPatch("{projectPhaseId}/finish")]
        public async Task<IActionResult> FinishProjectPhase(int projectPhaseId)
        {
            var result = await _projectPhasesService.FinishProjectPhase(projectPhaseId);
            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }


        [HttpPatch("{projectPhaseId}/estimated-cost")]
        public async Task<IActionResult> UpdateProjectPhaseEstimatedCost(int projectPhaseId, UpdateProjectPhaseEstimatedCostDto dto)
        {
            var result = await _projectPhasesService.UpdateProjectPhaseEstimatedCost(projectPhaseId,dto);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }
    }
}

