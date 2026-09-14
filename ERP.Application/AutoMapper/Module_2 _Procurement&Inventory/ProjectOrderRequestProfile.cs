using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class ProjectOrderRequestProfile:Profile
    {
        public ProjectOrderRequestProfile()
        {
            CreateMap<CreateProjectOrderRequestDto, ProjectOrderRequest>();
            CreateMap<RequestOrderMaterialsDto, OrderMaterials>();

            CreateMap<ProjectOrderRequest, OrderRequestDto>();

            CreateMap<OrderMaterials, OrderMaterialDto>()
                .ForMember(dest => dest.MaterialName, opt => opt.MapFrom(src => src.Material.Name));



        }
    }
}
