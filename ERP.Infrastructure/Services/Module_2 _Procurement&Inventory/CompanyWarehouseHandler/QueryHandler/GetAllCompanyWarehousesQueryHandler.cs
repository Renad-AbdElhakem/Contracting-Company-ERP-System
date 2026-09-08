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
    public class GetAllCompanyWarehousesQueryHandler
    : IRequestHandler<GetAllCompanyWarehousesQuery,
        GeneralResponse<List<CompanyWarehouseDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyWarehouseRepository _repository;

        public GetAllCompanyWarehousesQueryHandler(IMapper mapper,ICompanyWarehouseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GeneralResponse<List<CompanyWarehouseDto>>> Handle(GetAllCompanyWarehousesQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _repository.GetAllAsync();

            var result = _mapper.Map<List<CompanyWarehouseDto>>(warehouses);

            return GeneralResponse<List<CompanyWarehouseDto>>.Success(result);
        }
    }
}
