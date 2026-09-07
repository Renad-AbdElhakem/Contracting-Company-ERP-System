using AutoMapper;
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
    public class UpdateMaterialPriceCommandHandler : IRequestHandler<UpdateMaterialPriceCommand, GeneralResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ISupplierMaterialPriceRepository _repository;

        public UpdateMaterialPriceCommandHandler(IMapper mapper, ISupplierMaterialPriceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GeneralResponse<bool>> Handle(UpdateMaterialPriceCommand request, CancellationToken cancellationToken)
        {
            var materialPrice = await _repository.GetByIdAsync(request.Id);

            if (materialPrice == null)
                return GeneralResponse<bool>.Fail($"Material price with id {request.Id} not found");

            materialPrice.Price = request.MaterialPriceDto.Price;

            await _repository.UpdateAsync(materialPrice);

            return GeneralResponse<bool>.Success(true);
        }
    }
}
