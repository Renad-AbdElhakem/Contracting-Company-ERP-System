using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Presentation.Controllers.Module_1_Project_Site_Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllAsync();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientService.GetByIdAsync(id);

            if (client is null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            var id = await _clientService.CreateAsync(dto);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update( Guid id, UpdateClientDto dto)
        {
            var result = await _clientService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        
    }
}
