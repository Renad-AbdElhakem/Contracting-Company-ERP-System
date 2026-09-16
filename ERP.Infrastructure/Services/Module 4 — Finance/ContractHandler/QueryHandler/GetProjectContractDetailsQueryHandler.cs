using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.QueryHandler
{
    public class GetProjectContractDetailsQueryHandler : IRequestHandler<GetProjectContractDetailsQuery, GeneralResponse<ProjectContractDto>>
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public GetProjectContractDetailsQueryHandler(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<ProjectContractDto>> Handle(GetProjectContractDetailsQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectService.GetProjectWithInclude(request.projectId, c => c.ContractProject, c => c.Client);

            if (project.Data is null)
                return GeneralResponse<ProjectContractDto>.Fail($"Project with id {request.projectId} not found");

            if (project.Data.ContractProject is null)
                return GeneralResponse<ProjectContractDto>.Fail("Project does not have a contract");

            var projectContractDto = _mapper.Map<ProjectContractDto>(project.Data.ContractProject);
          
            return GeneralResponse<ProjectContractDto>.Success(projectContractDto);
        }
    }
}
