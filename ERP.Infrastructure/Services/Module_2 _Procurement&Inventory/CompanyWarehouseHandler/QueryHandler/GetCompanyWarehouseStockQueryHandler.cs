using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.CompanyWarehouseDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.CompanyWarehouseHandler.QueryHandler
{
    public class GetCompanyWarehouseStockQueryHandler : IRequestHandler<GetCompanyWarehouseStockQuery, GeneralResponse<List<CompanyWarehouseStockDto>>>
    {
        private readonly ICompanyWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetCompanyWarehouseStockQueryHandler(ICompanyWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<CompanyWarehouseStockDto>>> Handle(GetCompanyWarehouseStockQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetByIdWithStockAsync(request.warehouseId);

            if (warehouse == null)
                return GeneralResponse<List<CompanyWarehouseStockDto>>.Fail($"Company warehouse with id {request.warehouseId} not found");

            var result = _mapper.Map<List<CompanyWarehouseStockDto>>(warehouse.CompanyWarehouseStocks);

            return GeneralResponse<List<CompanyWarehouseStockDto>>.Success(result);
        }
    }
}
