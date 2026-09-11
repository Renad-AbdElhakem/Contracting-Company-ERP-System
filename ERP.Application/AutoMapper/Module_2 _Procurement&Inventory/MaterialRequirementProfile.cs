using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialRequirementsDtos;
using ERP.Domain.Model.Module_2___Procurement___Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class MaterialRequirementProfile : Profile
    {
        public MaterialRequirementProfile()
        {
            CreateMap<PhaseMaterialRequirement, MaterialRequirementDto>();

            CreateMap<CreateMaterialRequirementDto, PhaseMaterialRequirement>();

            CreateMap<UpdateMaterialRequirementDto, PhaseMaterialRequirement>();
        }
    }
}
