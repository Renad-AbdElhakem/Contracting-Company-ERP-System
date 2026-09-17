using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query
{
    public record GetSumContractPaymentRecordsByProjectIdQuery(Guid projectId) : IRequest<GeneralResponse<decimal?>>;
}
