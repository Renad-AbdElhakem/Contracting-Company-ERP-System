using ERP.Domain.Enum;
using ERP.Domain.Model.Module_2___Procurement___Inventory;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Material
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public MaterialType MaterialType { get; set; }

        //Navigation
        public ICollection<SupplierMaterialPrice> SupplierMaterials { get; set; } = new List<SupplierMaterialPrice>();
        public ICollection<PhaseMaterialRequirement> PhaseMaterialRequirements { get; set; } = new List<PhaseMaterialRequirement>();
        public ICollection<ProjectWarehouseStock>? ProjectWarehouseStocks { get; set; } = new List<ProjectWarehouseStock>();
        public ICollection<CompanyWarehouseStock>? CompanyWarehouseStocks { get; set; } = new List<CompanyWarehouseStock>();
        public ICollection<OrderMaterials>? OrderMaterials { get; set; } = new List<OrderMaterials>();
        public ICollection<MaterialPurchaseItem>? MaterialPurchaseItem { get; set; } = new List<MaterialPurchaseItem>();
    }
}
