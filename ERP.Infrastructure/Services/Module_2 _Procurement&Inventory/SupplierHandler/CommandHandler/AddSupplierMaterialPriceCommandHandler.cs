using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.SupplierDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler.CommandHandler
{
    public class AddSupplierMaterialPriceCommandHandler : IRequestHandler<AddSupplierMaterialPriceCommand, GeneralResponse<Guid>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public AddSupplierMaterialPriceCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<Guid>> Handle(AddSupplierMaterialPriceCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.GetByIdAsync(request.supplierId);

            if (supplier == null)
                return GeneralResponse<Guid>.Fail("Supplier not found");

            if (supplier.ContractEndDate != null)
                return GeneralResponse<Guid>.Fail("Supplier contract has ended");

            var suppliermaterial = _mapper.Map<SupplierMaterialPrice>(request.Dto);
         
            suppliermaterial.SupplierId = request.supplierId;

            supplier.SupplierMaterials.Add(suppliermaterial);

            await _supplierRepository.UpdateAsync(supplier);

           return GeneralResponse<Guid>.Success(suppliermaterial.Id);


        }
    }
}
