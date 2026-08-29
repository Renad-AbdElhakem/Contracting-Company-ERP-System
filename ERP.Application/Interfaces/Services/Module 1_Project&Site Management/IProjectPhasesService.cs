using ERP.Application.Dtos.Module_1_Project_Site_Management;
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
    }
}
