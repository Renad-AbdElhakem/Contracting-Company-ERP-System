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
    public class GetProjectWarehouseStockQueryHandler : IRequestHandler<GetProjectWarehouseStockQuery, GeneralResponse<List<ProjectWarehouseStockDto>>>
    {
        private readonly IProjectWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetProjectWarehouseStockQueryHandler(IProjectWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<ProjectWarehouseStockDto>>> Handle(GetProjectWarehouseStockQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdWithStockAsync(request.warehouseId);

            if (warehouse == null)
                return GeneralResponse<List<ProjectWarehouseStockDto>>.Fail($"Project warehouse with id {request.warehouseId} not found");

            var result = _mapper.Map<List<ProjectWarehouseStockDto>>(warehouse.ProjectWarehouseStocks);

            return GeneralResponse<List<ProjectWarehouseStockDto>>.Success(result);
        }
    }
}
