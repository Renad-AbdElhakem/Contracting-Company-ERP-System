using ERP.Domain.Model.Module_2__Procurement_Inventory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Query
{
   public record GetCompanyWarehouseStockByMaterialsIdsQuery(List<Guid>MaterialIds):IRequest<GeneralResponse<List<CompanyWarehouseStock>>>;
    
}
