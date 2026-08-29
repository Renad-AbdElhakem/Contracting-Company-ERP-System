using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<Project>> GetAllClientProjects(Guid clientId)
        {
            await _dbContext.Clients.Where(c => c.Id == clientId).ToListAsync();
            var clientProjects2 = await _dbContext.Projects.Where(c => c.ClientId == clientId).ToListAsync();
            return clientProjects2;
        }
    }
}
