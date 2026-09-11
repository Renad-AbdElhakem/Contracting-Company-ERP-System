using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
    public interface IProjectPhasesService
    {
        Task<GeneralResponse<bool>> UpdateProjectPhaseDates(int projectPhaseId, UpdateProjectPhaseDatesDto dto);
        Task<GeneralResponse<bool>> FinishProjectPhase(int projectPhaseId);
        Task<GeneralResponse<bool>> UpdateProjectPhaseEstimatedCost(int projectPhaseId,UpdateProjectPhaseEstimatedCostDto dto);
        Task<bool> IsExist(int projectPhaseId);
        Task<ProjectPhasesDetailsDto> GetById(int projectPhaseId);
        Task<List<MaterialConsumptionDetailsDto>> GetConsumptionsByProjectPhaseId(int projectPhaseId);
    }
}
