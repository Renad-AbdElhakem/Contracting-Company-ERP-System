using ERP.Application.Dtos.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_1_Project_Site_Management
{
   public interface IPhaseService
    {
        Task<GeneralResponse<PhaseDto>> GetPhaseByIdAsync(int phaseId);

        Task<GeneralResponse<List<PhaseDto>>> GetAllPhasesAsync();

        Task<GeneralResponse<int>> CreatePhaseAsync(CreatePhaseDto dto);
    }
}
