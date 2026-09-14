using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectOrderRequestService.Command
{
    public record SetOrderRequestUnderReviewCommand(int orderRequestId):IRequest<GeneralResponse<bool>>;
    
}
