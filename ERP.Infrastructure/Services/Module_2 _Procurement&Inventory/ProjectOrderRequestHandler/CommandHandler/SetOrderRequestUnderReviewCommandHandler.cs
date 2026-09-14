using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Command;
using ERP.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectOrderRequestHandler.CommandHandler
{
    public class SetOrderRequestUnderReviewCommandHandler : IRequestHandler<SetOrderRequestUnderReviewCommand, GeneralResponse<bool>>
    {
        private readonly IProjectOrderRequestRepository _projectOrderRequestRepository;

        public SetOrderRequestUnderReviewCommandHandler(IProjectOrderRequestRepository projectOrderRequestRepository)
        {
            _projectOrderRequestRepository = projectOrderRequestRepository;
        }


        public async Task<GeneralResponse<bool>> Handle(SetOrderRequestUnderReviewCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = await _projectOrderRequestRepository.GetByIdAsync(request.orderRequestId);

            if (orderRequest is null)
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderRequestId} not found");

            if (orderRequest.Status != RequestStatus.Pending)
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderRequestId} cannot be moved to under review because its status is {orderRequest.Status}");

            orderRequest.Status = RequestStatus.UnderReview;

            await _projectOrderRequestRepository.UpdateAsync(orderRequest);

            return GeneralResponse<bool>.Success(true, "Order request has been moved to under review successfully");
        }
    }
}
