using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Command;
using ERP.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectOrderRequestHandler.CommandHandler
{
    public class CancelOrderRequestCommandHandler : IRequestHandler<CancelOrderRequestCommand, GeneralResponse<bool>>
    {
        private readonly IProjectOrderRequestRepository _projectOrderRequestRepository;

        public CancelOrderRequestCommandHandler(IProjectOrderRequestRepository projectOrderRequestRepository)
        {
            _projectOrderRequestRepository = projectOrderRequestRepository;
        }
        public async Task<GeneralResponse<bool>> Handle(CancelOrderRequestCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = await _projectOrderRequestRepository.GetByIdAsync(request.orderRequestId);

            if (orderRequest is null)
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderRequestId} not found");

            if (orderRequest.Status is RequestStatus.Approved or RequestStatus.Rejected or RequestStatus.Cancelled)
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderRequestId} cannot be cancelled because its status is {orderRequest.Status}");

            orderRequest.Status = RequestStatus.Cancelled;
           
            await _projectOrderRequestRepository.UpdateAsync(orderRequest);

            return GeneralResponse<bool>.Success(true, "Order request has been cancelled successfully");



        }
    }
}
