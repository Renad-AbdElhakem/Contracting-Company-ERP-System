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
    public class CompanyWarehouseStockRepository:GenericRepository<CompanyWarehouseStock>, ICompanyWarehouseStockRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyWarehouseStockRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }



        public async Task UpdateRangeAsync(List<CompanyWarehouseStock> companyWarehouseStocks) 
        {
            _dbSet.UpdateRange(companyWarehouseStocks);
            await _dbContext.SaveChangesAsync();
        }
    }
}
