using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();

            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);

            if (role is null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateRoleDto dto)
        {
            var id = await _roleService.CreateAsync(dto);

            return Ok(id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateRoleDto dto)
        {
            var result =  await _roleService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

       
    }
}
