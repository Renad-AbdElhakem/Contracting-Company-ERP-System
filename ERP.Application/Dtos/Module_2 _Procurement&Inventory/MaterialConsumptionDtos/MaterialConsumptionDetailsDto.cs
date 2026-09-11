using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos
{
    public class MaterialConsumptionDetailsDto
    {
        public int Id { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal QuantityUsed { get; set; }
        public DateOnly Date { get; set; }
        public string? Note { get; set; }
        public int ProjectWarehouseStockId { get; set; }
     
    }
}
