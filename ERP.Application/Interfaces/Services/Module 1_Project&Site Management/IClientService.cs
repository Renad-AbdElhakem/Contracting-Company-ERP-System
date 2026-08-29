using ERP.Application.Dtos.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
    public interface IClientService
    {
        Task<Guid> CreateAsync(CreateClientDto dto);
        Task<ClientDto?> GetByIdAsync(Guid id);
        Task<List<ClientDto>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, UpdateClientDto dto);
     
    }
}
