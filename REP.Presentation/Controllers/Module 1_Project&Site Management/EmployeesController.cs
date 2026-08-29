using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? role,[FromQuery] string? department)
        {
            var employees =
                await _employeeService.GetAllAsync(role, department);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee =
                await _employeeService.GetByIdAsync(id);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var id = await _employeeService.CreateAsync(dto);

            return Ok(id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
        {
            var result =
                await _employeeService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/InactiveEmployee")]
        public async Task<IActionResult> InactiveEmployee(int id)
        {
            var result =
                await _employeeService.InactiveEmployee(id);

            if (!result)
                return NotFound();
            return Ok();
        }
    }
}
