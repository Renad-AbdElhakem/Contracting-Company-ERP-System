using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery
{
    public interface IProjectEquipmentService
    {
        Task<GeneralResponse<bool>> UnassignEquipmentAsync(Guid projectId);
        Task<GeneralResponse<bool>> UnassignEquipmentAsync(Guid projectId, Guid equipmentId);
    }
}
