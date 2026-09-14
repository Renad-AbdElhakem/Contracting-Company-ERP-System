using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Query;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.CompanyWarehouseHandler.QueryHandler
{
    public class GetCompanyWarehouseStockByMaterialsIdsQueryHandler : IRequestHandler<GetCompanyWarehouseStockByMaterialsIdsQuery, GeneralResponse<List<CompanyWarehouseStock>>>
    {
        private readonly ICompanyWarehouseRepository _companyWarehouseRepository;

        public GetCompanyWarehouseStockByMaterialsIdsQueryHandler(ICompanyWarehouseRepository companyWarehouseRepository)
        {
            _companyWarehouseRepository = companyWarehouseRepository;
        }
        public async Task<GeneralResponse<List<CompanyWarehouseStock>>> Handle(GetCompanyWarehouseStockByMaterialsIdsQuery request, CancellationToken cancellationToken)
        {
            var materialsStock = await _companyWarehouseRepository.GetCompanyStockByMaterialsIdsAsync(request.MaterialIds);
           
            return GeneralResponse<List<CompanyWarehouseStock>>.Success(materialsStock);

        }
    }
}
