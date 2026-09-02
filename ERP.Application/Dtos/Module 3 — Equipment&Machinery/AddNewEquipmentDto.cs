using ERP.Domain.Enum;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_3___Equipment_Machinery
{
    public class AddNewEquipmentDto
    {
        public string Name { get; set; } 
        public string Brand { get; set; } 
        public int ManufactureYear { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public EquipmentStatus EquipmentStatus { get; set; }
        public int SupplierId { get; set; }
    }
}
