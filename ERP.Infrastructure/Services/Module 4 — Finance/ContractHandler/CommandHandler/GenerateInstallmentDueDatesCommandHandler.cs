using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.CommandHandler
{
    public class GenerateInstallmentDueDatesCommandHandler : IRequestHandler<GenerateInstallmentDueDatesCommand, GeneralResponse<int>>
    {
        private readonly IContractPaymentRecordRepository _contractPaymentRecordRepository;
        private readonly IContractProjectRepository _contractProjectRepository;

        public GenerateInstallmentDueDatesCommandHandler(IContractPaymentRecordRepository contractPaymentRecordRepository,
                        IContractProjectRepository contractProjectRepository)
        {
            _contractPaymentRecordRepository = contractPaymentRecordRepository;
            _contractProjectRepository = contractProjectRepository;
        }

        public async Task<GeneralResponse<int>> Handle(GenerateInstallmentDueDatesCommand request, CancellationToken cancellationToken)
        {
            var contractProject = await _contractProjectRepository
                   .GetByIdWithInclude(request.ContractId, c => c.ContractPaymentPlans);

            if (contractProject is null)
                return GeneralResponse<int>.Fail($"Contract project with id {request.ContractId} not found");

            var paymentRecords = contractProject.ContractPaymentPlans.Select(plan => new ContractPaymentRecord
            {
                ContractId = request.ContractId,
                ContractInstallmentPlanId = plan.Id,
                AmountDue = contractProject.TotalAmount * (plan.Percentage / 100),
                DueDate = contractProject.SignDate.AddMonths(plan.MonthsAfterSignDate)
            }).ToList();

            await _contractPaymentRecordRepository.AddRangeAsync(paymentRecords);

            return GeneralResponse<int>.Success(paymentRecords.Count);
        }
    }
}
