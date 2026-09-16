using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using ERP.Domain.Model.Module_4___Finance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.CommandHandler
{
    public class AddInstallmentPlanCommandHandler : IRequestHandler<AddInstallmentPlanCommand, GeneralResponse<int>>
    {
        private readonly IContractProjectRepository _contractProjectRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddInstallmentPlanCommandHandler(IContractProjectRepository contractProjectRepository, IMediator mediator, IMapper mapper)
        {
            _contractProjectRepository = contractProjectRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<int>> Handle(AddInstallmentPlanCommand request, CancellationToken cancellationToken)
        {
            var contractProject = await _contractProjectRepository.GetByIdWithInclude(request.contractId, p => p.ContractPaymentPlans);

            if (contractProject is null)
                return GeneralResponse<int>.Fail("Contract not found");

            if (contractProject.ContractPaymentPlans.Any())
                return GeneralResponse<int>.Fail("Cannot add installment plan ");


            var resultPercentage = request.createInstallmentPlanDtos.Sum(p => p.Percentage);

            if (resultPercentage != 100)
                return GeneralResponse<int>.Fail("Percentage not equal 100%");

            var contractInstallmentPlanList = _mapper.Map<List<ContractInstallmentPlan>>(request.createInstallmentPlanDtos);

            contractInstallmentPlanList.ForEach(plan => plan.ContractId = request.contractId);

            contractProject.ContractPaymentPlans = contractInstallmentPlanList;


            await _contractProjectRepository.UpdateAsync(contractProject);

            var paymentPlan = await _mediator.Send(new GenerateInstallmentDueDatesCommand(request.contractId));

            return GeneralResponse<int>.Success(contractInstallmentPlanList.Count());
        }
    }
}
