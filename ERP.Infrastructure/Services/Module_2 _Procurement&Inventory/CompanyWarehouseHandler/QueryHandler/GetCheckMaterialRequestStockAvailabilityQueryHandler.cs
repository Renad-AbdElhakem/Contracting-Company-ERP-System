using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.StockTransfer;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Query;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.StockTransferService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.CompanyWarehouseHandler.QueryHandler
{
    public class GetCheckMaterialRequestStockAvailabilityQueryHandler : IRequestHandler<GetCheckMaterialRequestStockAvailabilityQuery, bool>
    {
        private readonly IMediator _mediator;
        private readonly ICompanyWarehouseStockRepository _companyWarehouseStockRepository;

        public GetCheckMaterialRequestStockAvailabilityQueryHandler(IMediator mediator, ICompanyWarehouseStockRepository companyWarehouseStockRepository)
        {
            _mediator = mediator;
            _companyWarehouseStockRepository = companyWarehouseStockRepository;
        }

        public async Task<bool> Handle(GetCheckMaterialRequestStockAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var materialsOrderedId = request.orderMaterials.Select(o => o.MaterialId).ToList();

            var materialsAtCompanyWarehouseStock = await _mediator.Send(new GetCompanyWarehouseStockByMaterialsIdsQuery(materialsOrderedId));

            List<bool> falseResults = new();
            List<CreateNewStockTransferDto> newStockTransferDtos = new();

            foreach (var materialStock in materialsAtCompanyWarehouseStock.Data)
            {
                foreach (var quantityRequest in request.orderMaterials)
                {
                    if (materialStock.Quantity < quantityRequest.Quantity)
                    {
                        falseResults.Add(false);
                        break;
                    }

                    materialStock.Quantity -= quantityRequest.Quantity;

                    var CreateNewStockTransferDto = new CreateNewStockTransferDto
                    {
                        CompanyWarehouseStockId = materialStock.Id,
                        OrderMaterialId = quantityRequest.Id,
                        Quantity = quantityRequest.Quantity,
                    };

                    newStockTransferDtos.Add(CreateNewStockTransferDto);
                }
            }

            if (!falseResults.Any())
            {
                await _companyWarehouseStockRepository.UpdateRangeAsync(materialsAtCompanyWarehouseStock.Data);
                await _mediator.Send(new CreateNewStockTransferCommand(newStockTransferDtos));
                return true;
            }

            return  false;
        }
    }
}
