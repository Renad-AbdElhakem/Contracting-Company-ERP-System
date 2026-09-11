using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos
{
    public class RequestedMaterialConsumptionDto
    {
        public Guid MaterialId { get; set; }
        public decimal RequestedQuantity { get; set; }
        public int ProjectPhaseId { get; set; }

    }
}
