using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler.QueryHandler
{
    public class GetMaterialPurchasesBySupplierQueryHandler : IRequestHandler<GetMaterialPurchasesBySupplierQuery, GeneralResponse<List<MaterialPurchaseDto>>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public GetMaterialPurchasesBySupplierQueryHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<List<MaterialPurchaseDto>>> Handle(GetMaterialPurchasesBySupplierQuery request, CancellationToken cancellationToken)
        {
            var purchases = await _supplierRepository.GetMaterialPurchaseDetailsBySupplierId(request.supplierId);

            if (purchases is null || !purchases.Any())
                return GeneralResponse<List<MaterialPurchaseDto>>.Fail("No material purchases found for this supplier.");

            var purchaseDtos = _mapper.Map<List<MaterialPurchaseDto>>(purchases);

            return GeneralResponse<List<MaterialPurchaseDto>>.Success(purchaseDtos);
        }
    }
}
