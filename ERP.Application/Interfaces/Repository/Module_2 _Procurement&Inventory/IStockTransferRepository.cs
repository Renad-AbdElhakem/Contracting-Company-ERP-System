using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory
{
    public interface IStockTransferRepository : IGenericRepository<StockTransfer>
    {
        Task AddRangeAsync(List<StockTransfer> stockTransfers);
    }
}
