using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_1_Project_Site_Management
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Role.RoleName))
              
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.Name));


        }
    }
}
