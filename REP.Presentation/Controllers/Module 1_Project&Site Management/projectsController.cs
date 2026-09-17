using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query;
using ERP.Application.Interfaces.Services.Module_4___Finance.ProjectFinancialSnapshotService.Command;
using ERP.Application.Interfaces.Services.Module_4___Finance.ProjectFinancialSnapshotService.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public projectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            var result = await _projectService.CreateAsync(dto);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }


        [HttpPatch("{id}/finish")]
        public async Task<IActionResult> FinishProject(Guid id)
        {
            var result = await _projectService.FinishProjectAsync(id);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);

        }
        [HttpPatch("{projectId}/Canncelled")]
        public async Task<IActionResult> CancelledProjectAsync(Guid projectId)
        {
            var result = await _projectService.CancelledProjectAsync(projectId);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);

        }


        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var result = await _projectService.GetAllProjects();

            return Ok(result);
        }
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            var result = await _projectService.GetProjectById(projectId);

            return Ok(result);
        }


        [HttpGet("{projectId}/phases")]
        public async Task<IActionResult> GetProjectPhases(Guid projectId)
        {
            var result = await _projectService.GetProjectPhasesDetails(projectId);

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }




        [HttpGet("{projectId}/employees")]
        public async Task<IActionResult> GetProjectEmployees(Guid projectId)
        {
            var result = await _projectService.GetProjectEmployeesDetails(projectId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }



        [HttpPatch("{projectId}/timeline")]
        public async Task<IActionResult> UpdateProjectTimeline(Guid projectId, UpdateProjectTimelineDto dto)
        {
            var result = await _projectService.UpdateProjectTimeline(projectId, dto);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }


        [HttpPatch("{projectId}")]
        public async Task<IActionResult> UpdateProject(Guid projectId, UpdateProjectDto dto)
        {
            var result = await _projectService.UpdateProject(projectId, dto);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }


        [HttpPost("{projectId}/employees")]
        public async Task<IActionResult> AssignEmployeeToProject(Guid projectId, AssignEmployeeToProjectDto dto)
        {
            var result = await _projectService.AssignEmployeeToProject(projectId, dto);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Message);

        }


        [HttpDelete("{projectId}/employees/{employeeId}")]
        public async Task<IActionResult> RemoveEmployeeFromProject(Guid projectId, int employeeId)
        {
            var result = await _projectService.RemoveEmployeeFromProject(projectId, employeeId);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }


        [HttpPost("{projectId}/phases")]
        public async Task<IActionResult> AddPhaseToProject(Guid projectId, AssignProjectPhaseDto dto)
        {
            var result = await _projectService.AddPhaseToProject(projectId, dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }



        [HttpPost("{projectId}/equipment")]
        public async Task<IActionResult> AssignEquipmentToProject(Guid projectId, AssignProjectEquipment dto)
        {
            var result = await _projectService.AssignEquipmentToProject(projectId, dto);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }



        [HttpGet("{projectId}/equipment")]
        public async Task<IActionResult> GetAllEquipmentByProjectId(Guid projectId)
        {
            var result = await _projectService.GetAllEquipmentByProjectId(projectId);

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }


        [HttpPost("{projectId}/warehouses")]
        public async Task<ActionResult<GeneralResponse<int>>> AssignProjectWarehouseToProject(Guid projectId, AddProjectWarehouseDto dto)
        {
            var response = await _mediator.Send(new AddWarehouseToProjectCommand(projectId, dto));

            return response.IsSuccess ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{projectId}/warehouses")]
        public async Task<IActionResult> GetAllProjectWarehouseByProjectId(Guid projectId)
        {
            var result = await _mediator.Send(new GetProjectWarehousesQuery(projectId));

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost("{projectId}/orderrequests")]
        public async Task<ActionResult<GeneralResponse<int>>> CreateProjectOrderRequest(Guid projectId, CreateProjectOrderRequestDto orderRequestDto)
        {
            var response = await _projectService.CreateProjectOrderRequestAsync(projectId, orderRequestDto);

            return response.IsSuccess ? Ok(response) : BadRequest(response.Message);
        }
        [HttpGet("{projectId}/orderrequests")]
        public async Task<IActionResult> GetOrderRequestsByProject(Guid projectId)
        {
            var result = await _mediator.Send(new GetOrderRequestsByProjectQuery(projectId));

            return Ok(result);
        }

        [HttpPost("{projectId}/Contract")]
        public async Task<IActionResult> CreateContract(Guid projectId,CreateContractDto createContractDto)
        {
            var result = await _mediator.Send(new CreateContractCommand(projectId,createContractDto));
          return result.IsSuccess? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("{projectId}/contract")]
        public async Task<IActionResult> GetProjectContract(Guid projectId)
        {
            var result = await _mediator.Send(new GetProjectContractDetailsQuery(projectId));

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }


        [HttpGet("{projectId}/contract/alldetails")]
        public async Task<ActionResult<GeneralResponse<AllProjectContractDetailsDto>>> GetAllProjectContractDetails(Guid projectId)
        {
            var result = await _mediator.Send(new GetAllProjectContractDetailsByProjectIdQuery(projectId));

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost("{projectId}/financial-snapshots")]
        public async Task<IActionResult> CreateFinancialSnapshot(Guid projectId, int  CreatedByEmployeeId)
        {
            var result = await _mediator.Send(new CreateFinancialSnapshotCommand(projectId, CreatedByEmployeeId));
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }


        [HttpGet("{projectId}/financial-snapshots")]
        public async Task<ActionResult<GeneralResponse<List<ProjectFinancialSnapshotDto>>>> GetAllProjectFinancialSnapshotDetails(Guid projectId)
        {
            var result = await _mediator.Send(new GetFinancialSnapshotsByProjectQuery(projectId));

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("{projectId}/financial-snapshots/latest")]
        public async Task<ActionResult<GeneralResponse<ProjectFinancialSnapshotDto>>> GetLatestProjectFinancialSnapshotDetails(Guid projectId)
        {
            var result = await _mediator.Send(new GetLatestFinancialSnapshotQuery(projectId));

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

    }
}
