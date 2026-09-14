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
    public class MarkProjectWarehouseAsFullCommandHandler : IRequestHandler<MarkProjectWarehouseAsFullCommand, GeneralResponse<bool>>
    {
        private readonly IProjectWarehouseRepository _projectWarehouseRepository;

        public MarkProjectWarehouseAsFullCommandHandler(IProjectWarehouseRepository projectWarehouseRepository)
        {
            _projectWarehouseRepository = projectWarehouseRepository;
        }
        public async Task<GeneralResponse<bool>> Handle(MarkProjectWarehouseAsFullCommand request, CancellationToken cancellationToken)
        {
            var projectwarehuse = await _projectWarehouseRepository.GetByIdAsync(request.projectwarehouseId);

            if (projectwarehuse is null)
                return GeneralResponse<bool>.Fail($"projecthouse with id {request.projectwarehouseId} not found");
            
            projectwarehuse.IsFull = true;
           
            await _projectWarehouseRepository.UpdateAsync(projectwarehuse);
           
            return GeneralResponse<bool>.Success(true, "Project warehouse has been marked as full successfully");
        }
    }
}
