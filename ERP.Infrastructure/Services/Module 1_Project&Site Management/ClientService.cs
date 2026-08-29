using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository,IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);

            await _clientRepository.AddAsync(client);

            return client.Id;
        }

        public async Task<ClientDto?> GetByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client is null)
                return null;

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<List<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();

            return _mapper.Map<List<ClientDto>>(clients);
        }

        public async Task<bool> UpdateAsync(Guid id,UpdateClientDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            if (client is null)
                return false;

            _mapper.Map(dto, client);

            await _clientRepository.UpdateAsync(client);

            return true;
        }

       
    }
}
