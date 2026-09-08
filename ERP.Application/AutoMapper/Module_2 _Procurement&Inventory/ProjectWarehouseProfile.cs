using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class ProjectWarehouseProfile:Profile
    {
        public ProjectWarehouseProfile()
        {

            CreateMap<AddProjectWarehouseDto, ProjectWarehouse>();
            
            CreateMap<ProjectWarehouse, ProjectWarehouseDto>();
            
            CreateMap<AddProjectWarehouseStockDto, ProjectWarehouseStock>();
            
            CreateMap<ProjectWarehouseStock, ProjectWarehouseStockDto>()
            .ForMember(dest => dest.ReceivedByEmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
        }
    }
}
