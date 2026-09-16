using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Query
{
    public record GetMaterialPurchaseByIdQuery(Guid materialPurchaseId):IRequest<GeneralResponse<MaterialPurchaseDto>>;
    
}
