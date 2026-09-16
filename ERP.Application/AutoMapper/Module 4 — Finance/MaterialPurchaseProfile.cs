using AutoMapper;
using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_4___Finance
{
    public class MaterialPurchaseProfile : Profile
    {
        public MaterialPurchaseProfile()
        {
            CreateMap<CreateMaterialPurchaseDto, MaterialPurchase>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.purchaseItemDtos.Sum(p => p.Quantity * p.UnitPrice)))
                .ForMember(
                    dest => dest.MaterialPurchaseItems,
                    opt => opt.MapFrom(src => src.purchaseItemDtos)
                );

            CreateMap<CreateMaterialPurchaseItemDto, MaterialPurchaseItem>();

            CreateMap<MaterialPurchase, MaterialPurchaseDto>();
            CreateMap<MaterialPurchaseItem, MaterialPurchaseItemDto>();
            CreateMap<MaterialPurchasePayment, MaterialPurchasePaymentDto>();







        }
    }
}
