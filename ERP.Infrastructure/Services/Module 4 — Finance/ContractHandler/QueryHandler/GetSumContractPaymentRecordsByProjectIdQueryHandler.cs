using ERP.Application;
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
    public class GetSumContractPaymentRecordsByProjectIdQueryHandler : IRequestHandler<GetSumContractPaymentRecordsByProjectIdQuery, GeneralResponse<decimal?>>
    {
        private readonly IContractProjectRepository _contractProjectRepository;

        public GetSumContractPaymentRecordsByProjectIdQueryHandler(IContractProjectRepository contractProjectRepository)
        {
            _contractProjectRepository = contractProjectRepository;
        }

        public async Task<GeneralResponse<decimal?>> Handle(GetSumContractPaymentRecordsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var totalPaid = await _contractProjectRepository.GetSumContractPaymentRecordsByProjectId(request.projectId);


            return GeneralResponse<decimal?>.Success(totalPaid);
        }
    }
}
