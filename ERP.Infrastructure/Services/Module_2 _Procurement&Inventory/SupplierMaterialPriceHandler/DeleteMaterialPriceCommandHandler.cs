using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierMaterialPriceService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierMaterialPriceHandler
{
    public class DeleteMaterialPriceCommandHandler : IRequestHandler<DeleteMaterialPriceCommand, GeneralResponse<bool>>
    {
        private readonly ISupplierMaterialPriceRepository _repository;

        public DeleteMaterialPriceCommandHandler(ISupplierMaterialPriceRepository repository)
        {
            _repository = repository;
        }

        public async Task<GeneralResponse<bool>> Handle(DeleteMaterialPriceCommand request, CancellationToken cancellationToken)
        {
            var materialPrice = await _repository.GetByIdAsync(request.Id);

            if (materialPrice == null)
                return GeneralResponse<bool>.Fail( $"Material price with id {request.Id} not found");

            await _repository.DeleteAsync(materialPrice);

            return GeneralResponse<bool>.Success(true);
        }
    }
}
