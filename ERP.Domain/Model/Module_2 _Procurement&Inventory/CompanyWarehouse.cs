using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model
{
    public class CompanyWarehouse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal? CapacitySquareMeters { get; set; }
        public bool IsFull { get; set; } = false;
        //Navigation
        public ICollection<Equipment>? Equipment { get; set; } = new List<Equipment>();
        public ICollection<CompanyWarehouseStock>? CompanyWarehouseStocks { get; set; } = new List<CompanyWarehouseStock>();
    }
}
