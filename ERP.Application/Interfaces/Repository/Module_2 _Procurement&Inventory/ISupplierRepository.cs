using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        IQueryable<Supplier> GetAllSupplier();
    }
}
