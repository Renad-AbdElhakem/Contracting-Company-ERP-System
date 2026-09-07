using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, GeneralResponse<SupplierDto>>
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public GetSupplierByIdQueryHandler(IMapper mapper,  ISupplierRepository supplierRepository)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }
        public async Task<GeneralResponse<SupplierDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByIdAsync(request.supplierId);
            if (supplier == null)
                return GeneralResponse<SupplierDto>.Fail($"supplier with id {request.supplierId} not found");
          
            var supplierDto = _mapper.Map<SupplierDto>(supplier);
            
            return GeneralResponse<SupplierDto>.Success(supplierDto);
        }
    }
}
