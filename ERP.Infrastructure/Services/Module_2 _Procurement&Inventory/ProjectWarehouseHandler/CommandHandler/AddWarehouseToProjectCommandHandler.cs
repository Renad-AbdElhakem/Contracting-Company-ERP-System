using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.CommandHandler
{
    public class AddWarehouseToProjectCommandHandler : IRequestHandler<AddWarehouseToProjectCommand, GeneralResponse<int>>
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public AddWarehouseToProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
        {

            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<int>> Handle(AddWarehouseToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.projectId);

            if (project == null)
                return GeneralResponse<int>.Fail($"Project with id {request.projectId} not found");

            if (project.ActualEndDate is not null)
                return GeneralResponse<int>.Fail($"Project with id {request.projectId} end at {project.ActualEndDate}");

            var warehouse = _mapper.Map<ProjectWarehouse>(request.WarehouseDto);

            project.ProjectWarehouses.Add(warehouse);

            await _projectRepository.UpdateAsync(project);

            return GeneralResponse<int>.Success(warehouse.Id);
        }
    }
}
