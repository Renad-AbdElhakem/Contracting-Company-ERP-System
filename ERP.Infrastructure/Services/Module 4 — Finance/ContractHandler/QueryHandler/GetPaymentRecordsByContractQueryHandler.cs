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
    public class GetPaymentRecordsByContractQueryHandler : IRequestHandler<GetPaymentRecordsByContractQuery, GeneralResponse<List<ContractPaymentRecordDto>>>
    {
        private readonly IContractProjectRepository _contractProjectRepository;
        private readonly IMapper _mapper;

        public GetPaymentRecordsByContractQueryHandler(IContractProjectRepository contractProjectRepository, IMapper mapper)
        {
            _contractProjectRepository = contractProjectRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<List<ContractPaymentRecordDto>>> Handle(GetPaymentRecordsByContractQuery request, CancellationToken cancellationToken)
        {
            var contractProject = await _contractProjectRepository.GetByIdWithInclude(request.contractPojectId, pr => pr.ContractPaymentRecords);

            if (contractProject == null)
                return GeneralResponse<List<ContractPaymentRecordDto>>.Fail($"Contract with id {request.contractPojectId} not found");

            var paymentsRecords = _mapper.Map<List<ContractPaymentRecordDto>>(contractProject.ContractPaymentRecords);

            return GeneralResponse<List<ContractPaymentRecordDto>>.Success(paymentsRecords);
        }
    }
}
