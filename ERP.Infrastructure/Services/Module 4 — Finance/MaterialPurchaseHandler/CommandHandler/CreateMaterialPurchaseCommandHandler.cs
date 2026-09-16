using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Queries;
using ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.MaterialPurchaseHandler.CommandHandler
{
    public class CreateMaterialPurchaseCommandHandler : IRequestHandler<CreateMaterialPurchaseCommand, GeneralResponse<Guid>>
    {
        private readonly IMaterialPurchaseRepository _materialPurchaseRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateMaterialPurchaseCommandHandler(IMaterialPurchaseRepository materialPurchaseRepository, IMapper mapper, IMediator mediator)

        {
            _materialPurchaseRepository = materialPurchaseRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<GeneralResponse<Guid>> Handle(CreateMaterialPurchaseCommand request, CancellationToken cancellationToken)
        {

            var supplier = await _mediator.Send(new GetSupplierByIdQuery(request.supplierId));
            if (!supplier.IsSuccess)
                return GeneralResponse<Guid>.Fail(supplier.Message);
            
            if (supplier.Data.ContractEndDate is not null)
                return GeneralResponse<Guid>.Fail("Supplier's contract has ended.");
          
            if (request.createMaterialPurchaseDto.purchaseItemDtos is null || !request.createMaterialPurchaseDto.purchaseItemDtos.Any())
                return GeneralResponse<Guid>.Fail("Material purchase must contain at least one item.");

            var purchase = _mapper.Map<MaterialPurchase>(request.createMaterialPurchaseDto);
            purchase.SupplierId = request.supplierId;
           
            await _materialPurchaseRepository.AddAsync(purchase);
           
            return GeneralResponse<Guid>.Success(purchase.Id, "Material purchase created successfully.");
        }
    }
}
