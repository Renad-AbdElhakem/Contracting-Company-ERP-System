using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Queries;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler.QueryHandler
{
    public class GetSupplierMaterialPricesQueryHandler : IRequestHandler<GetSupplierMaterialPricesQuery, GeneralResponse<List<SupplierMaterialPriceDto>>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierMaterialPriceRepository _supplierMaterialPriceRepository;
        private readonly IMapper _mapper;

        public GetSupplierMaterialPricesQueryHandler(ISupplierRepository supplierRepository, ISupplierMaterialPriceRepository supplierMaterialPriceRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _supplierMaterialPriceRepository = supplierMaterialPriceRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<SupplierMaterialPriceDto>>> Handle(GetSupplierMaterialPricesQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByIdAsync(request.supplierId);

            var materialprice = await _supplierMaterialPriceRepository.MaterialspriceBySupplierIdAsync(request.supplierId);
           
            var materialPriceDto = _mapper.Map<List<SupplierMaterialPriceDto>>(materialprice);
           
            return GeneralResponse<List<SupplierMaterialPriceDto>>.Success(materialPriceDto);
        }
    }
}
