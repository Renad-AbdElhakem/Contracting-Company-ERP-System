using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class ProjectPhasesService: IProjectPhasesService
    {
        private readonly IProjectPhaseRepository _projectPhaseRepository;

        public ProjectPhasesService(IProjectPhaseRepository projectPhaseRepository)
        {
             _projectPhaseRepository = projectPhaseRepository;
        }
        public async Task<GeneralResponse<bool>> UpdateProjectPhaseDates( UpdateProjectPhaseDatesDto dto)
        {
            var projectPhase = await _projectPhaseRepository
                .GetByIdAsync(dto.ProjectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<bool>.Fail(
                    $"Project phase with id {dto.ProjectPhaseId} not found.");

            if (projectPhase.FinishedDate is not null)
                return GeneralResponse<bool>.Fail(
                    "Cannot update dates of a finished phase.");

            projectPhase.StartDate = dto.StartDate;
            projectPhase.DueDate = dto.DueDate;

            await _projectPhaseRepository.UpdateAsync(projectPhase);

            return GeneralResponse<bool>.Success(true);
        }
        public async Task<GeneralResponse<bool>> FinishProjectPhase(FinishProjectPhaseDto dto)
        {
            var projectPhase = await _projectPhaseRepository.GetByIdAsync(dto.ProjectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<bool>.Fail(
                    $"Project phase with id {dto.ProjectPhaseId} not found.");

            if (projectPhase.FinishedDate is not null)
                return GeneralResponse<bool>.Fail(
                    "Project phase is already finished.");

            projectPhase.FinishedDate = DateOnly.FromDateTime(DateTime.UtcNow);
          
            //R
            //projectPhase.ActualCost = dto.ActualCost;

            //if (dto.ActualCost is not null)
            //    projectPhase.ActualCostCalculatedAt = DateTime.UtcNow;

            await _projectPhaseRepository.UpdateAsync(projectPhase);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<bool>> UpdateProjectPhaseEstimatedCost(UpdateProjectPhaseEstimatedCostDto dto)
        {
            var projectPhase = await _projectPhaseRepository.GetByIdAsync(dto.ProjectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<bool>.Fail(
                    $"Project phase with id {dto.ProjectPhaseId} not found.");

            if (projectPhase.FinishedDate is not null)
                return GeneralResponse<bool>.Fail("Cannot update estimated cost of a finished phase.");

            projectPhase.EstimatedCost = dto.EstimatedCost;

            await _projectPhaseRepository.UpdateAsync(projectPhase);

            return GeneralResponse<bool>.Success(true);
        }
    }
}
