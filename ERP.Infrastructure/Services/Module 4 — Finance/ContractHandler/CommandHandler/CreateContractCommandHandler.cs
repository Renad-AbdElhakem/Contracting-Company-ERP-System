using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using ERP.Infrastructure.Repository.Module_1_Project_Site_Management;
using ERP.Infrastructure.Services.Module_1_Project_Site_Management;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.CommandHandler
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, GeneralResponse<Guid>>
    {
        private readonly IProjectService _projectService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public CreateContractCommandHandler(IProjectService projectService, IClientService clientService, IMapper mapper)
        {
            _projectService = projectService;
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<Guid>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectService.GetProjectWithInclude(request.projectId, c => c.ContractProject);
            if (project is null)
                return GeneralResponse<Guid>.Fail($"project id with {request.projectId} not found");

            if (project.Data.ContractProject != null)
                return GeneralResponse<Guid>.Fail("project already have contract");

            var client = await _clientService.GetByIdAsync(request.CreateContractDto.ClientId);
            if (client is null)
                return GeneralResponse<Guid>.Fail($"Client id with {request.CreateContractDto.ClientId} not found");

            var contract = _mapper.Map<ContractProject>(request.CreateContractDto);
            contract.Status = ContractStatus.Active;

            project.Data.ContractProject = contract;

            await _projectService.UpdateAsync(project.Data);

            return GeneralResponse<Guid>.Success(contract.Id);

        }
    }
}
