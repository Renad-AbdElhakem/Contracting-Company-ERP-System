using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_1_Project_Site_Management
{
    public class ClientProfile :Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientDto, Client>();
            CreateMap<UpdateClientDto, Client>();
            CreateMap<Client, ClientDto>();

  
        }
    }
}
