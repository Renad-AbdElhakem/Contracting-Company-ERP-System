using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos
{
    public class AddSupplierMaterialPriceDto
    {

        public Guid MaterialId { get; set; }
        public decimal Price { get; set; }
       
    }
}
