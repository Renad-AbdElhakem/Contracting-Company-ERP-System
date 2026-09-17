using AutoMapper;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_4___Finance
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<CreateContractDto, ContractProject>();

            CreateMap<ContractProject, AllProjectContractDetailsDto>()
          .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Id));
          
            CreateMap<ContractProject, ProjectContractDto>()
                .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name));

            CreateMap<CreateInstallmentPlanDto, ContractInstallmentPlan>();
            CreateMap<ContractInstallmentPlan, ContractInstallmentPlanDto>();

            CreateMap<ContractPaymentRecord, ContractPaymentRecordDto>();

        }
    }
}
