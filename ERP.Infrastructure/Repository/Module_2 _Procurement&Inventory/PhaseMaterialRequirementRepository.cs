using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Domain.Model.Module_2___Procurement___Inventory;
using ERP.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_2__Procurement_Inventory
{
    public class PhaseMaterialRequirementRepository : GenericRepository<PhaseMaterialRequirement>, IPhaseMaterialRequirementRepository
    {
        private readonly ApplicationDbContext _context;

        public PhaseMaterialRequirementRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
