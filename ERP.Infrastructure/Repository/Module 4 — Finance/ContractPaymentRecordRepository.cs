using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Domain.Model;
using ERP.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_4___Finance
{
    public class ContractPaymentRecordRepository : GenericRepository<ContractPaymentRecord>, IContractPaymentRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractPaymentRecordRepository(ApplicationDbContext context) : base(context)
        {
          _context = context;
        }

        public async Task AddRangeAsync(List<ContractPaymentRecord> record) 
        {
        
            _dbSet.AddRange(record);
          await  _context.SaveChangesAsync();
        }
    }
}
