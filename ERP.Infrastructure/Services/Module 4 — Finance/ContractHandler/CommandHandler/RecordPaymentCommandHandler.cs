using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ContractHandler.CommandHandler
{
    public class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand, GeneralResponse<bool>>
    {
        private readonly IContractPaymentRecordRepository _contractPaymentRecordRepository;

        public RecordPaymentCommandHandler(IContractPaymentRecordRepository contractPaymentRecordRepository)
        {
            _contractPaymentRecordRepository = contractPaymentRecordRepository;
        }
        public async Task<GeneralResponse<bool>> Handle(RecordPaymentCommand request, CancellationToken cancellationToken)
        {
            var paymentRecord = await _contractPaymentRecordRepository.GetByIdAsync(request.paymentRecordId);

            if (paymentRecord is null)
                return GeneralResponse<bool>.Fail($"payment with id {request.paymentRecordId} not found");

            if (paymentRecord.PaidDate is not null)
                return GeneralResponse<bool>.Fail("This payment has already been recorded.");
           
            if (request.addPaymentRecordDto.AmountPaid != paymentRecord.AmountDue)
                return GeneralResponse<bool>.Fail("Amount paid must match amount due.");
           
            paymentRecord.AmountPaid = request.addPaymentRecordDto.AmountPaid;
            paymentRecord.PaidDate = request.addPaymentRecordDto.PaidDate;

           await _contractPaymentRecordRepository.UpdateAsync(paymentRecord);

            return GeneralResponse<bool>.Success(true, "Payment recorded successfully.");

        }
    }
}
