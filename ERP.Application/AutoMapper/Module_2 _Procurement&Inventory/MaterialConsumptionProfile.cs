using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class MaterialConsumptionProfile : Profile
    {
        public MaterialConsumptionProfile()
        {
            CreateMap<RequestedMaterialConsumptionDto, MaterialConsumption>()
                .ForMember(dest => dest.QuantityUsed, opt => opt.MapFrom(src => src.RequestedQuantity));

            CreateMap<MaterialConsumption, MaterialConsumptionDetailsDto>()
                .ForMember(dest => dest.MaterialName, opt => opt.MapFrom(src => src.Material.Name));


        }
    }
}
