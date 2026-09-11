using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialConsumptionDtos;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model._1_Project_Site_Management;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class ProjectPhasesService : IProjectPhasesService
    {
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IMapper _mapper;

        public ProjectPhasesService(IProjectPhaseRepository projectPhaseRepository, IMapper mapper)
        {
            _projectPhaseRepository = projectPhaseRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<bool>> UpdateProjectPhaseDates(int projectPhaseId, UpdateProjectPhaseDatesDto dto)
        {
            var projectPhase = await _projectPhaseRepository.GetByIdAsync(projectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<bool>.Fail(
                    $"Project phase with id {projectPhaseId} not found.");

            if (projectPhase.FinishedDate is not null)
                return GeneralResponse<bool>.Fail("Cannot update dates of a finished phase.");

            projectPhase.StartDate = dto.StartDate;
            projectPhase.DueDate = dto.DueDate;

            await _projectPhaseRepository.UpdateAsync(projectPhase);

            return GeneralResponse<bool>.Success(true);
        }
        public async Task<GeneralResponse<bool>> FinishProjectPhase(int projectPhaseId)
        {
            var projectPhase = await _projectPhaseRepository.GetByIdAsync(projectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<bool>.Fail($"Project phase with id {projectPhaseId} not found.");

            if (projectPhase.FinishedDate is not null)
                return GeneralResponse<bool>.Fail("Project phase is already finished.");

            projectPhase.FinishedDate = DateOnly.FromDateTime(DateTime.UtcNow);

            //R
            //projectPhase.ActualCost = dto.ActualCost;

            //if (dto.ActualCost is not null)
            //    projectPhase.ActualCostCalculatedAt = DateTime.UtcNow;

            await _projectPhaseRepository.UpdateAsync(projectPhase);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<bool>> UpdateProjectPhaseEstimatedCost(int projectPhaseId, UpdateProjectPhaseEstimatedCostDto dto)
        {
            var projectPhase = await _projectPhaseRepository.GetByIdAsync(projectPhaseId);

            if (projectPhase is null)
                return GeneralResponse<bool>.Fail(
                    $"Project phase with id {projectPhaseId} not found.");

            if (projectPhase.FinishedDate is not null)
                return GeneralResponse<bool>.Fail("Cannot update estimated cost of a finished phase.");

            projectPhase.EstimatedCost = dto.EstimatedCost;

            await _projectPhaseRepository.UpdateAsync(projectPhase);

            return GeneralResponse<bool>.Success(true);
        }


        public async Task<bool> IsExist(int projectPhaseId)
        {
            return await _projectPhaseRepository.IsExistAsync(projectPhaseId);
        }

        public async Task<ProjectPhasesDetailsDto> GetById(int projectPhaseId)
        {
            var projectPhase = await _projectPhaseRepository.GetByIdAsync(projectPhaseId);
            return _mapper.Map<ProjectPhasesDetailsDto>(projectPhase);
        }


        public async Task<List<MaterialConsumptionDetailsDto>> GetConsumptionsByProjectPhaseId(int projectPhaseId)
        {
            var projectPhase = await _projectPhaseRepository.GetAllMaterialConsumptionsByProjectPhaseIdAsync(projectPhaseId);

            return _mapper.Map<List<MaterialConsumptionDetailsDto>>(projectPhase.MaterialConsumptions);
        }

      


    }
}
