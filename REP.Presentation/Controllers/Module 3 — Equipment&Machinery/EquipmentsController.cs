using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_3___Equipment_Machinery
{
    [Route("api/[controller]")]
    [ApiController]

    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EquipmentsDto>>> GetEquipments()
        {
            var result = await _equipmentService.GetEquipments();
            return Ok(result);
        }
        [HttpGet("/Filter")]
        public async Task<ActionResult<List<EquipmentsDto>>> GetEquipmentsByFilter([FromQuery] GetEquipmentsWithFilterDto withFilterDto)
        {
            var result = await _equipmentService.GetEquipmentsByFilter(withFilterDto);

            return Ok(result);
        }

        [HttpGet("{equipmentId}")]
        public async Task<ActionResult<EquipmentsDto>> GetEquipmentById(Guid equipmentId)
        {
            var result = await _equipmentService.GetEquipmentById(equipmentId);

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddEquipment(AddNewEquipmentDto dto)
        {
            var result = await _equipmentService.AddEquipmentsAsync(dto);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpDelete("{equipmentId}")]
        public async Task<ActionResult> SoftDelete(Guid equipmentId)
        {
            var result = await _equipmentService.SoftDeleteAsync(equipmentId);

            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<bool>> Update(Guid id, UpdateEquipmentDto dto)
        {
            var result = await _equipmentService.UpdateAsync(id, dto);
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpPost("{equipmentId}/maintenance")]
        public async Task<ActionResult<Guid>> AddMaintenance(Guid equipmentId, AddMaintenanceDto dto)
        {
            var result = await _equipmentService.AddMaintenanceAsync(equipmentId, dto);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpGet("{equipmentId}/maintenance")]
        public async Task<ActionResult<MaintenanceDto>> GetMaintenancesHistory(Guid equipmentId)
        {
            var result = await _equipmentService.GetMaintenceHistoryByEquipmentId(equipmentId);
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);

        }

        [HttpGet("{equipmentId}/projects")]
        public async Task<ActionResult<List<ProjectEquipmentDto>>> GetProjectsHistory(Guid equipmentId)
        {
            var result = await _equipmentService.GetProjectsHistoryByEquipmentId(equipmentId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);

        }


        [HttpPatch("{equipmentId}/available")]
        public async Task<IActionResult> MarkAsAvailable(Guid equipmentId)
        {
            var result = await _equipmentService.MarkAsAvailableAsync(equipmentId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpPatch("available")]
        public async Task<IActionResult> MarkAsAvailable(List<Guid> equipmentId)
        {
            var result = await _equipmentService.MarkAsAvailableAsync(equipmentId);
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }

        [HttpPatch("{equipmentId}/in-use")]
        public async Task<IActionResult> MarkAsInUse(Guid equipmentId)
        {
            var result = await _equipmentService.MarkAsInUseAsync(equipmentId);
            return result.IsSuccess ? Ok(result) : NotFound(result.Message);
        }





    }

}
