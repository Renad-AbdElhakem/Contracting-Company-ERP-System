using ERP.Domain.Enum;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Brand { get; set; }=string.Empty;
        public int ManufactureYear { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public EquipmentStatus EquipmentStatus{ get; set; }

        //Navigation
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int? CompanyWarehouseId { get; set; }   
        public CompanyWarehouse? CompanyWarehouse { get; set; }
        public ICollection<ProjectEquipment>? ProjectEquipment { get; set; } = new List<ProjectEquipment>();
        public ICollection<Equipment_Maintenance>? Equipment_Maintenances { get; set; } = new List<Equipment_Maintenance>();
        public ICollection<EquipmentPurchaseItem>? EquipmentPurchaseItem { get; set; } = new List<EquipmentPurchaseItem>();
    }
}
