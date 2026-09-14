using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Query;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Command;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectOrderRequestHandler.CommandHandler
{
    public class AddMaterialToOrderCommandHandler : IRequestHandler<AddMaterialToOrderCommand, GeneralResponse<bool>>
    {
        private readonly IProjectOrderRequestRepository _projectOrderRequestRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddMaterialToOrderCommandHandler(IProjectOrderRequestRepository projectOrderRequestRepository, IMediator mediator  ,IMapper mapper)
        {
            _projectOrderRequestRepository = projectOrderRequestRepository;
           _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<bool>> Handle(AddMaterialToOrderCommand request, CancellationToken cancellationToken)
        {
            var orderRequest = await _projectOrderRequestRepository.GetByIdAsync(request.orderId);

            if (orderRequest is null)
                return GeneralResponse<bool>.Fail($"Order request with  id {request.orderId} not found ");

            if (orderRequest.Status != RequestStatus.Pending)
                return GeneralResponse<bool>.Fail($"Cannot create order for project with  order status : {orderRequest.Status}");

            var material = await _mediator.Send(new GetMaterialByIdQuery(request.orderMaterialsDto.MaterialId));
            
            if (!material.IsSuccess)
                return GeneralResponse<bool>.Fail($"Material with  id {request.orderMaterialsDto.MaterialId} not found ");

            var newMaterialOrdered = _mapper.Map<OrderMaterials>(request.orderMaterialsDto);
        
            orderRequest.OrderMaterials.Add(newMaterialOrdered);
            
            await _projectOrderRequestRepository.UpdateAsync(orderRequest);
            
            return GeneralResponse<bool>.Success(true, "new material ordered request added ");

        }
    }
}
