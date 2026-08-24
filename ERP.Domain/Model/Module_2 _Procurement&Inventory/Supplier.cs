using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime ContractSigningDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

        //Navigation
        public ICollection<SupplierMaterialPrice>? SupplierMaterials { get; set; }= new List<SupplierMaterialPrice>();
        public ICollection<Equipment> ?Equipments { get; set; }= new List<Equipment>();
        public ICollection<MaterialPurchase> ?MaterialPurchases { get; set; }= new List<MaterialPurchase>();
        public ICollection<EquipmentPurchase> ? EquipmentPurchases { get; set; }= new List<EquipmentPurchase>();

    }
}
