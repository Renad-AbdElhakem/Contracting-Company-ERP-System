using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Query;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query;
using ERP.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectOrderRequestHandler.CommandHandler
{
    public class MarkOrderAsApprovedCommandHandler : IRequestHandler<ValidateOrderRequestCommand, GeneralResponse<bool>>
    {
        private readonly IProjectOrderRequestRepository _projectOrderRequestRepository;
        private readonly IMediator _mediator;

        public MarkOrderAsApprovedCommandHandler(IProjectOrderRequestRepository projectOrderRequestRepository, IMediator mediator)
        {
            _projectOrderRequestRepository = projectOrderRequestRepository;
            _mediator = mediator;
        }

        public async Task<GeneralResponse<bool>> Handle(ValidateOrderRequestCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = await _projectOrderRequestRepository.GetByIdWithInclude(request.orderId, o => o.OrderMaterials);

            if (orderRequest is null)
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderId} not found");

            if (orderRequest.Status != RequestStatus.Pending && orderRequest.Status != RequestStatus.UnderReview)
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderId} cannot be validated. Current status: {orderRequest.Status} ");

            if (!orderRequest.OrderMaterials.Any())
                return GeneralResponse<bool>.Fail($"Order request with id {request.orderId} has no materials");

            var CheckMaterialStockAvailability = await _mediator.Send(new GetCheckMaterialRequestStockAvailabilityQuery(orderRequest.OrderMaterials.ToList()));

            orderRequest.Status = !CheckMaterialStockAvailability ? RequestStatus.Rejected : RequestStatus.Approved;

            await _projectOrderRequestRepository.UpdateAsync(orderRequest);

            return GeneralResponse<bool>.Success(true, $"Order request status is {orderRequest.Status}");
        }
    }
}
