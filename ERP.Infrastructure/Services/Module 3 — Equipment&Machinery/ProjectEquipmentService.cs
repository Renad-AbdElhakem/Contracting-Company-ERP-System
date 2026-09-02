using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_3___Equipment_Machinery
{
    public class ProjectEquipmentService : IProjectEquipmentService
    {
        private readonly IProjectEquipmentRepository _projectEquipmentRepository;
        private readonly IEquipmentService _equipmentService;

        public ProjectEquipmentService(IProjectEquipmentRepository projectEquipmentRepository, IEquipmentService equipmentService)
        {
            _projectEquipmentRepository = projectEquipmentRepository;
            _equipmentService = equipmentService;
        }



        public async Task<GeneralResponse<bool>> UnassignEquipmentAsync(Guid projectId)
        {
            var projectEquipments = await _projectEquipmentRepository.GetAllWithConditionAsync(p => p.ProjectId == projectId && p.EndDate == null);

            projectEquipments.ForEach(x => x.EndDate = DateOnly.FromDateTime(DateTime.UtcNow));

            var reponse = await _equipmentService.MarkAsAvailableAsync(projectEquipments.Select(e => e.EquipmentId).ToList());

            if (!reponse.IsSuccess)
                return GeneralResponse<bool>.Fail(reponse.Message);

            await _projectEquipmentRepository.UpdateRangeAsync(projectEquipments);

            return GeneralResponse<bool>.Success(true);

        }
        public async Task<GeneralResponse<bool>> UnassignEquipmentAsync(Guid projectId, Guid equipmentId)
        {
            var projectEquipment = await _projectEquipmentRepository.GetWithConditionAsync(p => p.ProjectId == projectId && p.EquipmentId == equipmentId
                                                                                                            && p.EndDate == null);

            projectEquipment.EndDate = DateOnly.FromDateTime(DateTime.UtcNow);

            var reponse = await _equipmentService.MarkAsAvailableAsync(projectEquipment.EquipmentId);

            if (!reponse.IsSuccess)
                return GeneralResponse<bool>.Fail(reponse.Message);

            await _projectEquipmentRepository.UpdateAsync(projectEquipment);

            return GeneralResponse<bool>.Success(true);

        }
    }
}
