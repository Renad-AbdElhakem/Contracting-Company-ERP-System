using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler.CommandHandler
{
    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, GeneralResponse<int>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public UpdateSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public  async Task<GeneralResponse<int>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateSupplierDto is null)
                return GeneralResponse<int>.Fail("must have data");

            var supplier = await _supplierRepository.GetByIdAsync(request.supplierId);
            
            if (supplier is null)
                return GeneralResponse<int>.Fail($"Supplier with id {request.supplierId} not found");

            if (!string.IsNullOrEmpty(request.UpdateSupplierDto.Name))
                supplier.Name = request.UpdateSupplierDto.Name;
                    
            if (!string.IsNullOrEmpty(request.UpdateSupplierDto.Address))
                supplier.Address = request.UpdateSupplierDto.Address;
                    
            if (!string.IsNullOrEmpty(request.UpdateSupplierDto.Phone))
                supplier.Phone = request.UpdateSupplierDto.Phone;
                    
            if (!string.IsNullOrEmpty(request.UpdateSupplierDto.Email))
                supplier.Email = request.UpdateSupplierDto.Email;

           await _supplierRepository.UpdateAsync(supplier);

            return GeneralResponse<int>.Success();
                    
        }
    }
}
