using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos
{
    public record CreateNewSupplierDto
    {
        public string Name { get; set; } 
        public string Phone { get; set; }
        public string Email { get; set; } 
        public string Address { get; set; } 
        public DateTime ContractSigningDate { get; set; }
    }
}
