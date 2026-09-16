using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Command
{
    public record RecordMaterialPurchasePaymentCommand(Guid materialPurchaseId, RecordMaterialPurchasePaymentDto Dto) : IRequest<GeneralResponse<Guid>>;

}
