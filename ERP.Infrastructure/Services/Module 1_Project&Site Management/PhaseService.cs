using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_1_Project_Site_Management;
using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_1_Project_Site_Management
{
    public class PhaseService : IPhaseService
    {
        private readonly IPhaseRepository _phaseRepository;
        private readonly IMapper _mapper;

        public PhaseService(IPhaseRepository phaseRepository,IMapper mapper)
        {
           _phaseRepository = phaseRepository;
           _mapper = mapper;
        }


        public async Task<GeneralResponse<PhaseDto>> GetPhaseByIdAsync(int phaseId)
        {
            var phase = await _phaseRepository.GetByIdAsync(phaseId);

            if (phase is null)
                return GeneralResponse<PhaseDto>.Fail($"Phase with id {phaseId} not found.");

            var phaseDto = _mapper.Map<PhaseDto>(phase);

            return GeneralResponse<PhaseDto>.Success(phaseDto);
        }
       
        public async Task<GeneralResponse<List<PhaseDto>>> GetAllPhasesAsync()
        {
            var phases = await _phaseRepository.GetAllAsync();

            var phaseDtos = _mapper.Map<List<PhaseDto>>(phases);

            return GeneralResponse<List<PhaseDto>>.Success(phaseDtos);
        }


        public async Task<GeneralResponse<int>> CreatePhaseAsync(CreatePhaseDto dto)
        {
            if (dto is null)
                return GeneralResponse<int>.Fail("Phase data is required.");

            var phase = _mapper.Map<Phase>(dto);

            await _phaseRepository.AddAsync(phase);

            return GeneralResponse<int>.Success(phase.Id);
        }
    }
}
