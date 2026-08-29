using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Domain.Model.Module_1_Project_Site_Management;
using ERP.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repository.Module_1_Project_Site_Management
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context)  : base(context)
        {
        }
    }
}
