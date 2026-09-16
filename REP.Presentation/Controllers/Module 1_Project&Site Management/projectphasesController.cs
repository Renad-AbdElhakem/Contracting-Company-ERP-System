using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using ERP.Application.Dtos.Module_4___Finance.InternalExpensesDtos;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Command;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialRequirementsService.Query;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.InternalExpensesService.Query;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Infrastructure.Services.Module_4___Finance.InternalExpensesHandler.QueryHandler;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectphasesController : ControllerBase
    {
        private readonly IProjectPhasesService _projectPhasesService;
        private readonly IMediator _mediator;

        public projectphasesController(IProjectPhasesService projectPhasesService, IMediator mediator)
        {
            _projectPhasesService = projectPhasesService;
            _mediator = mediator;
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
            var result = await _projectPhasesService.UpdateProjectPhaseEstimatedCost(projectPhaseId, dto);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }

        [HttpPost("{projectPhaseId}/materialrequirements")]
        public async Task<IActionResult> AddMaterialRequirementToProjectPhaseAsync(int projectPhaseId, CreateMaterialRequirementDto dto)
        {
            var result = await _mediator.Send(new CreateMaterialRequirementCommand(projectPhaseId, dto));

            return CreatedAtAction("GetMaterialRequirementById", "MaterialRequirements",
                                  new { materialRequirementId = result.Data }, result);
        }

        [HttpGet("{projectPhaseId}/materialrequirements")]
        public async Task<IActionResult> GetByPhase(int projectPhaseId)
        {
            var result = await _mediator.Send(new GetMaterialRequirementsByPhaseQuery(projectPhaseId));

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("{projectPhaseId}/materialconsumption")]
        public async Task<ActionResult<List<MaterialConsumptionDetailsDto>>> GetConsumptionsByProjectPhaseId(int projectPhaseId)
        {
            var result = await _projectPhasesService.GetConsumptionsByProjectPhaseId(projectPhaseId);

            return Ok(result);
        }


        [HttpPost("{projectPhaseId}/expenses")]
        public async Task<IActionResult> CreateProjectExpense(int projectPhaseId, CreateProjectExpenseDto dto)
        {
            var result = await _mediator.Send(new CreateProjectExpenseCommand(projectPhaseId, dto));
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{projectPhaseId}/expenses")]
        public async Task<IActionResult> GetProjectExpensesByPhase(int projectPhaseId)
        {
            var result = await _mediator.Send(new GetProjectExpensesByPhaseQuery(projectPhaseId));

            return Ok(result);
        }
        [HttpGet("{projectPhaseId}/cost-variance")]
        public async Task<IActionResult> GetProjectPhaseCostVarianceByPhaseId(int projectPhaseId)
        {
            var result = await _mediator.Send(new GetPhaseCostVarianceQuery(projectPhaseId));

            return Ok(result);
        }


    }
}

