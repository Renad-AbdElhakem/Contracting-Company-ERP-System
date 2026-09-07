using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos
{
   public class SupplierMaterialPriceDto
    {
        public Guid Id { get; set; }
        public string supplierName { get; set; }
        public string MaterialName { get; set; }
        public decimal Price { get; set; }
    }
}
