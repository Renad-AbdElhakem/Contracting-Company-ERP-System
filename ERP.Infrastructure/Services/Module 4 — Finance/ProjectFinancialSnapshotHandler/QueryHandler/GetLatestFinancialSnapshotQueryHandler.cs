using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ProjectFinancialSnapshotService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ProjectFinancialSnapshotHandler.QueryHandler
{
    internal class GetLatestFinancialSnapshotQueryHandler : IRequestHandler<GetLatestFinancialSnapshotQuery, GeneralResponse<ProjectFinancialSnapshotDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectFinancialSnapshotRepository _projectFinancialSnapshotRepository;
        private readonly IMapper _mapper;

        public GetLatestFinancialSnapshotQueryHandler(IProjectRepository projectRepository, 
            IProjectFinancialSnapshotRepository projectFinancialSnapshotRepository,IMapper mapper)
        {
            _projectRepository = projectRepository;
            _projectFinancialSnapshotRepository = projectFinancialSnapshotRepository;
           _mapper = mapper;
        }
        public async Task<GeneralResponse<ProjectFinancialSnapshotDto>> Handle(GetLatestFinancialSnapshotQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.projectId);

            if (project is null)
                return GeneralResponse<ProjectFinancialSnapshotDto>.Fail($"Project with id {request.projectId} not found");

            var financial = await _projectFinancialSnapshotRepository.GetLastestFinancialSnapshotsByProjectId(request.projectId);
            var financialDto = _mapper.Map<ProjectFinancialSnapshotDto>(financial);

            return GeneralResponse<ProjectFinancialSnapshotDto>.Success(financialDto);
        }
    }
}
