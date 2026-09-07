using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.SupplierService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.SupplierHandler
{
    public class CreateNewSupplierCommandHandler : IRequestHandler<CreateNewSupplierCommand, GeneralResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public CreateNewSupplierCommandHandler(IMapper mapper, ISupplierRepository supplierRepository)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateNewSupplierCommand request, CancellationToken cancellationToken)
        {

            if (request.NewSupplierDto == null)
                return GeneralResponse<int>.Fail("Must have data");

            var supplier = _mapper.Map<Supplier>(request.NewSupplierDto);

            await _supplierRepository.AddAsync(supplier);

            return GeneralResponse<int>.Success(supplier.Id);
        }
    }
}
