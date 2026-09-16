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
    public class GetLatePaymentsCountQueryHandler : IRequestHandler<GetLatePaymentsCountQuery, int>
    {
        private readonly IContractProjectRepository _contractProjectRepository;

        public GetLatePaymentsCountQueryHandler(IContractProjectRepository contractProjectRepository)
        {
            _contractProjectRepository = contractProjectRepository;
        }
        public async Task<int> Handle(GetLatePaymentsCountQuery request, CancellationToken cancellationToken)
        {
            return await _contractProjectRepository.GetLatePaymentsCountAsync(request.contractProjectId);
        }
    }
}
