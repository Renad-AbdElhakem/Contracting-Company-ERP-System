using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query;
using ERP.Infrastructure.Repository.Module_4___Finance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.QueryHandler
{
    public class GetAllProjectContractDetailsByProjectIdQueryHandler : IRequestHandler<GetAllProjectContractDetailsByProjectIdQuery, GeneralResponse<AllProjectContractDetailsDto>>
    {
        private readonly IContractProjectRepository _contractProjectRepository;
        private readonly IMapper _mapper;
        public GetAllProjectContractDetailsByProjectIdQueryHandler(IContractProjectRepository contractProjectRepository, IMapper mapper)
        {
            _contractProjectRepository = contractProjectRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<AllProjectContractDetailsDto>> Handle(GetAllProjectContractDetailsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var contractProject = await _contractProjectRepository.GetAllProjectContractDetails(request.projectId);

            if (contractProject is null)
                return GeneralResponse<AllProjectContractDetailsDto>.Fail($"Contract for project with id {request.projectId} not found");

            var contractDetailsDto = _mapper.Map<AllProjectContractDetailsDto>(contractProject);

            return GeneralResponse<AllProjectContractDetailsDto>.Success(contractDetailsDto);
        }
    }
}
