using ERP.Application.Dtos.Module_1_Project_Site_Management;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery
{
    public interface IMaintenanceService
    {
        Task<GeneralResponse<Guid>> AddNewMaintenance(Guid equipmentId, AddMaintenanceDto dto);
        Task<GeneralResponse<bool>> UpdateMaintenanceAsync(Guid maintenanceId, UpdateMaintenanceDto dto);
        Task<GeneralResponse<Guid>> CompleteAsync(Guid maintenancetId, EndMaintenanceDto dto);
        Task<GeneralResponse<int>> AssignEmployeeToMaintenanceAsync(Guid maintenanceId, AssignEmployeeToMaintenanceDto dto);
        Task<GeneralResponse<bool>> RemoveEmployeeFromMaintenanceAsync( Guid maintenanceId, int employeeId);
        Task<GeneralResponse<List<EmployeeSummaryDto>>> GetEmployeesAtMaintenanceByIdAsync(Guid maintenanceId);
    }
}
