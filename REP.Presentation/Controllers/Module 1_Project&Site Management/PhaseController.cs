using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhaseController : ControllerBase
    {
        private readonly IPhaseService _phaseService;

        public PhaseController(IPhaseService phaseService)
        {
            _phaseService = phaseService;
        }


        [HttpGet("{phaseId}")]
        public async Task<IActionResult> GetPhaseById(int phaseId)
        {
            var result = await _phaseService.GetPhaseByIdAsync(phaseId);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhases()
        {
            var result = await _phaseService.GetAllPhasesAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhase(CreatePhaseDto dto)
        {
            var result = await _phaseService.CreatePhaseAsync(dto);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);
        }
    }
}
