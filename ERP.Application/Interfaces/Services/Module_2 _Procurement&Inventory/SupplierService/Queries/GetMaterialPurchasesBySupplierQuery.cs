using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Queries
{
    public record GetMaterialPurchasesBySupplierQuery(int supplierId): IRequest<GeneralResponse<List<MaterialPurchaseDto>>>;
}
