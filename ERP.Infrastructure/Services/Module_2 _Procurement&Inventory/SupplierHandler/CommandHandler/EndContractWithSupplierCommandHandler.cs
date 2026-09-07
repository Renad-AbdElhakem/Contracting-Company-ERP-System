using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler.CommandHandler
{
    public class EndContractWithSupplierCommandHandler : IRequestHandler<EndContractWithSupplierCommand, GeneralResponse<int>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public EndContractWithSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<GeneralResponse<int>> Handle(EndContractWithSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request.SupplierDto == null)
                return GeneralResponse<int>.Fail("must have data");

            var supplier = await _supplierRepository.GetByIdAsync(request.supplierId);

            supplier.ContractEndDate = request.SupplierDto.ContractEndDate;

            await _supplierRepository.UpdateAsync(supplier);

          return  GeneralResponse<int>.Success();
        }
    }
}
