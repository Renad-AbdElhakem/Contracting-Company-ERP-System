using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<CreateMaterialDto, Material>();
            CreateMap<Material, MaterialDto>();
            CreateMap<SupplierMaterialPrice, SupplierMaterialPriceDto>()
                .ForMember(dest => dest.MaterialName, opt => opt.MapFrom(src => src.Material.Name))
                .ForMember(dest => dest.supplierName, opt => opt.MapFrom(src => src.Supplier.Name));
        }
    }
}
