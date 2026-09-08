using ERP.Application.Dtos.Module_2__Procurement_Inventory.CompanyWarehouseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Command
{
    public record AddStockToCompanyWarehouseCommand(int warehouseId, AddCompanyWarehouseStockDto StockDto): IRequest<GeneralResponse<int>>;
}
