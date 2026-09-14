using AutoMapper;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.CompanyWarehouseDtos;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.StockTransfer;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_2__Procurement_Inventory
{
    public class CompanyWarehouseProfile : Profile
    {
        public CompanyWarehouseProfile()
        {
            CreateMap<CreateCompanyWarehouseDto, CompanyWarehouse>();

            CreateMap<UpdateCompanyWarehouseDto, CompanyWarehouse>();

            CreateMap<CompanyWarehouse, CompanyWarehouseDto>();

            CreateMap<AddCompanyWarehouseStockDto, CompanyWarehouseStock>();

            CreateMap<CompanyWarehouseStock, CompanyWarehouseStockDto>()
           .ForMember(dest => dest.ReceivedByEmployeeName, opt => opt.MapFrom(src => src.Employee.Name));

            CreateMap<CreateNewStockTransferDto, StockTransfer>();




        }
    }
}
