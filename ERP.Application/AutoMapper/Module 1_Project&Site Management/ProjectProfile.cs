using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<CreateProjectDto, Project>();

            CreateMap<Project, ProjectDetailsDto>()
                .ForMember( dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name)
                );

            //--------------------ProjectPhase-------------------------
            CreateMap<ProjectPhase, ProjectPhasesDetailsDto>()
                .ForMember(
                    dest => dest.PhaseName,
                    opt => opt.MapFrom(src => src.Phase.Name)
                );

            CreateMap<AssignProjectPhaseDto, ProjectPhase>();
            //----------------------------------------------------

            CreateMap<ContractProject, ProjectContractDto>();

            //-----------------------------

            CreateMap<AssignEmployeeToProjectDto, ProjectEmployee>();

            CreateMap<ProjectEmployee, ProjectEmployeesDetailsDto>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name));


        }
    }
}
