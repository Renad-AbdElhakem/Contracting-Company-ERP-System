using AutoMapper;
using ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos;
using ERP.Domain.Model.Module_4___Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.AutoMapper.Module_4___Finance
{
    public class ProjectFinancialSnapshotProfile : Profile
    {
        public ProjectFinancialSnapshotProfile()
        {
            CreateMap<ProjectFinancialSnapshot,ProjectFinancialSnapshotDto>();
        }
    }
}
