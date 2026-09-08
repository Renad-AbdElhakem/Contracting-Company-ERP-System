using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.QueryHandler
{
    public class GetProjectWarehouseByIdQueryHandler : IRequestHandler<GetProjectWarehouseByIdQuery, GeneralResponse<ProjectWarehouseDto>>
    {
        private readonly IProjectWarehouseRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectWarehouseByIdQueryHandler(IProjectWarehouseRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<ProjectWarehouseDto>> Handle(GetProjectWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var projectWarehouse = await _repository.GetByIdAsync(request.warehouseId);

            if (projectWarehouse == null)
                return GeneralResponse<ProjectWarehouseDto>.Fail($"Project warehouse with id {request.warehouseId} not found");

            var projectWarehouseDto = _mapper.Map<ProjectWarehouseDto>(projectWarehouse);

            return GeneralResponse<ProjectWarehouseDto>.Success(projectWarehouseDto);
        }
    }
}
