using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery
{
   public interface IEquipmentService
    {
        Task<List<EquipmentsDto>> GetEquipments();
        Task<List<EquipmentsDto>> GetEquipmentsByFilter(GetEquipmentsWithFilterDto withFilterDto);
        Task<GeneralResponse<EquipmentsDto>> GetEquipmentById(Guid equipmentId);
        Task<GeneralResponse<Guid>> AddEquipmentsAsync(AddNewEquipmentDto dto); 
        Task<GeneralResponse<bool>> SoftDeleteAsync(Guid id);
        Task<GeneralResponse<bool>> UpdateAsync(Guid id, UpdateEquipmentDto dto);

        Task<GeneralResponse<Guid>> AddMaintenanceAsync(Guid equipmentId, AddMaintenanceDto dto);
        Task<GeneralResponse<List<MaintenanceDto>>> GetMaintenceHistoryByEquipmentId(Guid equipmentId);
        Task<GeneralResponse<List<ProjectEquipmentDto>>> GetProjectsHistoryByEquipmentId(Guid equipmentId);

        Task<GeneralResponse<bool>> MarkAsAvailableAsync(Guid equipmentId);
        Task<GeneralResponse<bool>> MarkAsAvailableAsync(List<Guid> equipmentId);
        Task<GeneralResponse<bool>> MarkAsInUseAsync(Guid equipmentId);
   }
}
