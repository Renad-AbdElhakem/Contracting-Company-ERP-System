using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.CommandHandler
{
    public class UpdateProjectWarehouseCommandHandler : IRequestHandler<UpdateProjectWarehouseCommand, GeneralResponse<bool>>
    {
        private readonly IProjectWarehouseRepository _repository;

        public UpdateProjectWarehouseCommandHandler(IProjectWarehouseRepository repository)
        {
            _repository = repository;
        }

        public async Task<GeneralResponse<bool>> Handle(UpdateProjectWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _repository.GetByIdAsync(request.warehouseId);

            if (warehouse == null)
                return GeneralResponse<bool>.Fail($"Project warehouse with id {request.warehouseId} not found");

            if (request.WarehouseDto.Name != null)
                warehouse.Name = request.WarehouseDto.Name;

            if (request.WarehouseDto.Location != null)
                warehouse.Location = request.WarehouseDto.Location;

            if (request.WarehouseDto.CapacitySquareMeters.HasValue)
                warehouse.CapacitySquareMeters = request.WarehouseDto.CapacitySquareMeters;

            await _repository.UpdateAsync(warehouse);

            return GeneralResponse<bool>.Success(true);
        }
    }
}
