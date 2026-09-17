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
    internal class GetFinancialSnapshotsByProjectQueryHandler : IRequestHandler<GetFinancialSnapshotsByProjectQuery, GeneralResponse<List<ProjectFinancialSnapshotDto>>>
    {
        private readonly IProjectFinancialSnapshotRepository _projectFinancialSnapshotRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetFinancialSnapshotsByProjectQueryHandler(IProjectFinancialSnapshotRepository projectFinancialSnapshotRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _projectFinancialSnapshotRepository = projectFinancialSnapshotRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<List<ProjectFinancialSnapshotDto>>> Handle(GetFinancialSnapshotsByProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.projectId);

            if (project is null)
                return GeneralResponse<List<ProjectFinancialSnapshotDto>>.Fail($"Project with id {request.projectId} not found");

            var financial = await _projectFinancialSnapshotRepository.GetFinancialSnapshotsByProjectId(request.projectId);
            var financialDto = _mapper.Map<List<ProjectFinancialSnapshotDto>>(financial);

            return GeneralResponse<List<ProjectFinancialSnapshotDto>>.Success(financialDto);

        }
    }
}
