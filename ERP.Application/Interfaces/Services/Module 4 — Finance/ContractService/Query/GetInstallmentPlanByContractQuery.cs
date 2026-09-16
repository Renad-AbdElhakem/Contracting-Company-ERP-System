using ERP.Application.Dtos.Module_4___Finance.ContractDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query
{
    public record GetInstallmentPlanByContractQuery(Guid contractId) : IRequest<GeneralResponse<List<ContractInstallmentPlanDto>>>;
}
