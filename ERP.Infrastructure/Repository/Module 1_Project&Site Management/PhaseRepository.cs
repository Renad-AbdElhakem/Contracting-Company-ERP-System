using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class PhaseRepository:GenericRepository<Phase>,IPhaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PhaseRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
