using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_3___Equipment_Machinery
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<Equipment, EquipmentsDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));

            CreateMap<AddNewEquipmentDto, Equipment>();

            CreateMap<AddMaintenanceDto, Equipment_Maintenance>();
         
            CreateMap<Equipment_Maintenance, MaintenanceDto>();
            CreateMap<ProjectEquipment, ProjectEquipmentDto>();
           
            CreateMap<MaintenanceEmployee, EmployeeSummaryDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));
        }
    }
}
