using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectOrderRequestDtos;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectOrderRequestHandler.QueryHandler
{
    public class GetOrderRequestsByProjectQueryHandler : IRequestHandler<GetOrderRequestsByProjectQuery, GeneralResponse<List<OrderRequestDto>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetOrderRequestsByProjectQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<OrderRequestDto>>> Handle(GetOrderRequestsByProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetProjectWithOrderRequestsAsync(request.projectId);

            if (project is null)
                return GeneralResponse<List<OrderRequestDto>>.Fail($"Project with id {request.projectId} not found");

            var orderRequestDtos = _mapper.Map<List<OrderRequestDto>>(project.ProjectOrderRequests);

            return GeneralResponse<List<OrderRequestDto>>.Success(orderRequestDtos);
        }
    }
}
