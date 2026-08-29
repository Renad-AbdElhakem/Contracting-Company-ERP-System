using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
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


        [HttpPatch("dates")]
        public async Task<IActionResult> UpdateProjectPhaseDates(
            UpdateProjectPhaseDatesDto dto)
        {
            var result = await _projectPhasesService.UpdateProjectPhaseDates(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPatch("finish")]
        public async Task<IActionResult> FinishProjectPhase(
            FinishProjectPhaseDto dto)
        {
            var result = await _projectPhasesService.FinishProjectPhase(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPatch("estimated-cost")]
        public async Task<IActionResult> UpdateProjectPhaseEstimatedCost(
            UpdateProjectPhaseEstimatedCostDto dto)
        {
            var result = await _projectPhasesService.UpdateProjectPhaseEstimatedCost(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

