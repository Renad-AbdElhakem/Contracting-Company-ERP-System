using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.MaterialPurchaseHandler.CommandHandler
{
    public class RecordMaterialPurchasePaymentCommandHandler
     : IRequestHandler<RecordMaterialPurchasePaymentCommand, GeneralResponse<Guid>>
    {
        private readonly IMaterialPurchaseRepository _materialPurchaseRepository;

        public RecordMaterialPurchasePaymentCommandHandler(IMaterialPurchaseRepository materialPurchaseRepository)
        {
            _materialPurchaseRepository = materialPurchaseRepository;
        }

        public async Task<GeneralResponse<Guid>> Handle(RecordMaterialPurchasePaymentCommand request, CancellationToken cancellationToken)
        {
            var materialPurchase = await _materialPurchaseRepository
                .GetByIdWithInclude(request.materialPurchaseId, mp => mp.Payments);

            if (materialPurchase is null)
                return GeneralResponse<Guid>.Fail($"MaterialPurchase with id {request.materialPurchaseId} not found");

            var alreadyPaid = materialPurchase.Payments.Sum(p => p.AmountPaid ?? 0);
            var newTotal = alreadyPaid + request.Dto.AmountPaid;

            if (newTotal > materialPurchase.TotalPrice)
                return GeneralResponse<Guid>.Fail("Payment exceeds the total amount due for this purchase.");

            var payment = new MaterialPurchasePayment
            {
                MaterialPurchaseId = materialPurchase.Id,
                AmountDue = materialPurchase.TotalPrice,
                AmountPaid = request.Dto.AmountPaid,
                PaidDate = request.Dto.PaidDate,
                DueDate = request.Dto.DueDate
            };

            materialPurchase.Payments.Add(payment);

            await _materialPurchaseRepository.UpdateAsync(materialPurchase);

            return GeneralResponse<Guid>.Success(payment.Id, "Payment recorded successfully.");
        }
    }
}
