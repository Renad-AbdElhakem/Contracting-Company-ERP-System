using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_2__Procurement_Inventory
{
    public class ProjectOrderRequestRepository : GenericRepository<ProjectOrderRequest>, IProjectOrderRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectOrderRequestRepository(ApplicationDbContext context) : base(context)
        {
           _context = context;
        }
        public async Task<ProjectOrderRequest?> GetByIdWithInclude(int orderRequestId, params Expression<Func<ProjectOrderRequest, object>>[] Includes)
        {
            var query = _dbSet.AsQueryable();

            foreach (var include in Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == orderRequestId);
        }


        public async Task<ProjectOrderRequest?> GetOrderRequestWithMaterialsByIdAsync(int orderRequestId)
        {
            return await _context.ProjectOrderRequests
                .Include(x => x.OrderMaterials)
                    .ThenInclude(x => x.Material)
                .FirstOrDefaultAsync(x => x.Id == orderRequestId);
        }


    }
}
