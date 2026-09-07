using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_2__Procurement_Inventory
{
    public class SupplierMaterialPriceRepository : GenericRepository<SupplierMaterialPrice>, ISupplierMaterialPriceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SupplierMaterialPriceRepository(ApplicationDbContext context) : base(context)
        {
           _dbContext = context;
        }
        public async Task<SupplierMaterialPrice?> BestMaterialpriceAsync(Guid materialId)
        {


            var materialprice0 = await _dbContext.SupplierMaterialPrices
                                                .Where(m => m.MaterialId == materialId)
                                                .Include(s => s.Supplier)
                                                .Include(m=>m.Material)
                                                .ToListAsync();

            return materialprice0.MinBy(p => p.Price);

        }
        public async Task<List<SupplierMaterialPrice>> MaterialspriceBySupplierIdAsync(int supplierId)
        {

            return await _dbContext.SupplierMaterialPrices
                                                .Where(m => m.SupplierId== supplierId)
                                                .Include(s => s.Supplier)
                                                .Include(m=>m.Material)
                                                .ToListAsync();

        }
    }
}
