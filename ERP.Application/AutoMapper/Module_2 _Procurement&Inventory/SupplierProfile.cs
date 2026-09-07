using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class SupplierProfile :Profile
    {
        public SupplierProfile()
        {
            CreateMap<CreateNewSupplierDto, Supplier>();
            CreateMap<Supplier, SupplierDto>();
            CreateMap<AddSupplierMaterialPriceDto, SupplierMaterialPrice>();
        }
    }
}
