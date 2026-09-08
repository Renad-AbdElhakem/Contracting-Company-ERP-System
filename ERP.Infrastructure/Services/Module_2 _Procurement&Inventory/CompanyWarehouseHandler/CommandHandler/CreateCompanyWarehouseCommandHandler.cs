using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.CompanyWarehouseService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.CompanyWarehouseHandler.CommandHandler
{
    public class CreateCompanyWarehouseCommandHandler
    : IRequestHandler<CreateCompanyWarehouseCommand, GeneralResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyWarehouseRepository _repository;

        public CreateCompanyWarehouseCommandHandler(IMapper mapper, ICompanyWarehouseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateCompanyWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (request.NewCompanyWarehouseDto is null)
                return GeneralResponse<int>.Fail("Must have data");

            var warehouse = _mapper.Map<CompanyWarehouse>(request.NewCompanyWarehouseDto);

            await _repository.AddAsync(warehouse);

            return GeneralResponse<int>.Success(warehouse.Id);
        }
    }
}
