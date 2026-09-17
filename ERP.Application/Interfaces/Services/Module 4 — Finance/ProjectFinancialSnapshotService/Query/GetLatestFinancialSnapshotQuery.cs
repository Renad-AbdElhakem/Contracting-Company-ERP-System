using ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.ProjectFinancialSnapshotService.Query
{
    public record GetLatestFinancialSnapshotQuery(Guid projectId) : IRequest<GeneralResponse<ProjectFinancialSnapshotDto>>;


}
