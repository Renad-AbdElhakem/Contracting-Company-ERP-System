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
    public class GetCompanyWarehouseByIdQueryHandler : IRequestHandler<GetCompanyWarehouseByIdQuery,GeneralResponse<CompanyWarehouseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyWarehouseRepository _repository;

        public GetCompanyWarehouseByIdQueryHandler(IMapper mapper, ICompanyWarehouseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GeneralResponse<CompanyWarehouseDto>> Handle( GetCompanyWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _repository.GetByIdAsync(request.Id);

            if (warehouse == null)
                return GeneralResponse<CompanyWarehouseDto>.Fail( $"Company warehouse with id {request.Id} not found");

            var dto = _mapper.Map<CompanyWarehouseDto>(warehouse);

            return GeneralResponse<CompanyWarehouseDto>.Success(dto);
        }
    }
}
