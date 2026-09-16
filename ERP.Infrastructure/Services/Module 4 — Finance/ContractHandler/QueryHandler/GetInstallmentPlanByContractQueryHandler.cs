using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.QueryHandler
{
    public class GetInstallmentPlanByContractQueryHandler : IRequestHandler<GetInstallmentPlanByContractQuery, GeneralResponse<List<ContractInstallmentPlanDto>>>
    {
        private readonly IContractProjectRepository _contractProjectRepository;
        private readonly IMapper _mapper;

        public GetInstallmentPlanByContractQueryHandler(IContractProjectRepository contractProjectRepository, IMapper mapper)
        {
            _contractProjectRepository = contractProjectRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<ContractInstallmentPlanDto>>> Handle(GetInstallmentPlanByContractQuery request, CancellationToken cancellationToken)
        {
            var contract = await _contractProjectRepository.GetByIdWithInclude(request.contractId, c => c.ContractPaymentPlans);

            if (contract is null)
                return GeneralResponse<List<ContractInstallmentPlanDto>>.Fail($"Contract with id {request.contractId} not found");

            var installmentPlans = _mapper.Map<List<ContractInstallmentPlanDto>>(contract.ContractPaymentPlans);

            return GeneralResponse<List<ContractInstallmentPlanDto>>.Success(installmentPlans);
        }
    }
}
