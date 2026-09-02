using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_3___Equipment_Machinery
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEquipmentController : ControllerBase
    {
        private readonly IProjectEquipmentService _projectEquipmentService;

        public ProjectEquipmentController(IProjectEquipmentService projectEquipmentService)
        {
            _projectEquipmentService = projectEquipmentService;
        }


        [HttpDelete("{projectId}/equipment/{equipmentId}")]
        public async Task<IActionResult> UnassignProjectEquipment(Guid projectId, Guid equipmentId)
        {

            var result = await _projectEquipmentService.UnassignEquipmentAsync(projectId, equipmentId);
            return result.IsSuccess ? NoContent() : NotFound(result.Message);
        }


    }
}
