using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management
{
    public interface IClientRepository :IGenericRepository<Client>
    {
        
        Task<List<Project>> GetAllClientProjects(Guid clientId);
    }
}
