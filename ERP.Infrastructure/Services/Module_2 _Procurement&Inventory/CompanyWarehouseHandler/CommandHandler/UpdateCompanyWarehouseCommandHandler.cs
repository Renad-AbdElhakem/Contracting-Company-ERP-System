using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.CompanyWarehouseHandler.CommandHandler
{
    public class UpdateCompanyWarehouseCommandHandler
     : IRequestHandler<UpdateCompanyWarehouseCommand, GeneralResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyWarehouseRepository _repository;

        public UpdateCompanyWarehouseCommandHandler(IMapper mapper, ICompanyWarehouseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GeneralResponse<bool>> Handle(UpdateCompanyWarehouseCommand request,CancellationToken cancellationToken)
        {
            var warehouse = await _repository.GetByIdAsync(request.Id);

            if (warehouse == null)
                return GeneralResponse<bool>.Fail($"Company warehouse with id {request.Id} not found");

            if (!string.IsNullOrEmpty( request.CompanyWarehouseDto.Name))
                warehouse.Name = request.CompanyWarehouseDto.Name;

            if (!string.IsNullOrEmpty(request.CompanyWarehouseDto.Location))
                warehouse.Location = request.CompanyWarehouseDto.Location;

            if (request.CompanyWarehouseDto.CapacitySquareMeters.HasValue)
                warehouse.CapacitySquareMeters = request.CompanyWarehouseDto.CapacitySquareMeters;

            await _repository.UpdateAsync(warehouse);

            return GeneralResponse<bool>.Success(true);
        }
    }
}
