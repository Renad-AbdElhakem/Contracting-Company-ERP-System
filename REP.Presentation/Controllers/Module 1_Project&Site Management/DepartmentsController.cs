using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(
            IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments =
                await _departmentService.GetAllAsync();

            return Ok(departments);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department =
                await _departmentService.GetByIdAsync(id);

            if (department is null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateDepartmentDto dto)
        {
            var id =
                await _departmentService.CreateAsync(dto);

            return Ok(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateDepartmentDto dto)
        {
            var result = await _departmentService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/Inactive")]
        public async Task<IActionResult>InactiveDepartment(int id)
        {
            var result = await _departmentService.InactiveDepartment(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
