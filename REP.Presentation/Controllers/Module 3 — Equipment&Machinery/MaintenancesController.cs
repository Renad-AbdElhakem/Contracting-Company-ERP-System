using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_3___Equipment_Machinery
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenancesController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly IEquipmentService _equipmentService;

        public MaintenancesController(IMaintenanceService maintenanceService, IEquipmentService equipmentService)
        {
            _maintenanceService = maintenanceService;
            _equipmentService = equipmentService;
        }


        [HttpPatch("{maintenanceId}")]
        public async Task<ActionResult<bool>> UpdateMaintenance(Guid maintenanceId, UpdateMaintenanceDto dto)
        {
            var result = await _maintenanceService.UpdateMaintenanceAsync(maintenanceId, dto);

            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }


        [HttpPatch("{maintenanceId}/complete")]
        public async Task<ActionResult> CompleteMaintenance(Guid maintenanceId,EndMaintenanceDto request)
        {
            var maintenanceResponse = await _maintenanceService.CompleteAsync(maintenanceId, request);

            if (!maintenanceResponse.IsSuccess)
                return NotFound(maintenanceResponse.Message);

            var equipmentResponse = await _equipmentService.MarkAsAvailableAsync(maintenanceResponse.Data);

            if (!equipmentResponse.IsSuccess)
                return NotFound(equipmentResponse.Message);

            return NoContent();
        }


        [HttpPost("{maintenanceId}/employees")]
        public async Task<ActionResult> AssignEmployeeToMaintenance(Guid maintenanceId, AssignEmployeeToMaintenanceDto dto)
        {
            var result = await _maintenanceService.AssignEmployeeToMaintenanceAsync(maintenanceId, dto);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpDelete("{maintenanceId}/employees/{employeeId}")]
        public async Task<ActionResult> RemoveEmployeeFromMaintenance(Guid maintenanceId, int employeeId)
        {
            var result = await _maintenanceService.RemoveEmployeeFromMaintenanceAsync(maintenanceId, employeeId);
          
            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }

        [HttpGet("{maintenanceId}")]
        public async Task<ActionResult<List<EmployeeSummaryDto>>> GetEmployeesAtMaintenanceById(Guid maintenanceId)
        {
            var result = await _maintenanceService.GetEmployeesAtMaintenanceByIdAsync(maintenanceId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

    }
}
