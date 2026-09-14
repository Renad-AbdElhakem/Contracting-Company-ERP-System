using ERP.Application.Dtos.Module_2__Procurement_Inventory.StockTransfer;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using ERP.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_2__Procurement_Inventory
{
    public class StockTransferRepository : GenericRepository<StockTransfer>, IStockTransferRepository
    {
        private readonly ApplicationDbContext _context;

        public StockTransferRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<StockTransfer> stockTransfers)
        {
           _dbSet.AddRange(stockTransfers);
            await _context.SaveChangesAsync();
        }
    }
}
