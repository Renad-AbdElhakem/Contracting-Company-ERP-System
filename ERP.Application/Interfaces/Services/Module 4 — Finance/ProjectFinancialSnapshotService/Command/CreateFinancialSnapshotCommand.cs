using ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.ProjectFinancialSnapshotService.Command
{
    public record CreateFinancialSnapshotCommand(Guid ProjectId ,int CreatedByEmployeeId) :IRequest<GeneralResponse<Guid>>;


}
