using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectOrderRequestHandler.QueryHandler
{
    public class GetOrderRequestByIdQueryHandler : IRequestHandler<GetOrderRequestByIdQuery, GeneralResponse<OrderRequestDto>>
    {
        private readonly IProjectOrderRequestRepository _projectOrderRequestRepository;
        private readonly IMapper _mapper;

        public GetOrderRequestByIdQueryHandler(IProjectOrderRequestRepository projectOrderRequestRepository, IMapper mapper)
        {
            _projectOrderRequestRepository = projectOrderRequestRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<OrderRequestDto>> Handle(GetOrderRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var orderRequest = await _projectOrderRequestRepository.GetOrderRequestWithMaterialsByIdAsync(request.orderRequestId);

            if (orderRequest is null)
                return GeneralResponse<OrderRequestDto>.Fail($"Order request with id {request.orderRequestId} not found");

            var orderRequestDto = _mapper.Map<OrderRequestDto>(orderRequest);

            return GeneralResponse<OrderRequestDto>.Success(orderRequestDto);
        }
    }
}
