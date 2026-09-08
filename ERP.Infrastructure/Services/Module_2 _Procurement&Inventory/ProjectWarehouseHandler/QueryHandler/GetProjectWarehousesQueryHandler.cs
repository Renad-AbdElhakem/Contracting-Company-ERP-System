using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.ProjectWarehouseDtos;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.ProjectWarehouseService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.ProjectWarehouseHandler.QueryHandler
{
    public class GetProjectWarehousesQueryHandler : IRequestHandler<GetProjectWarehousesQuery, GeneralResponse<List<ProjectWarehouseDto>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectWarehousesQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<List<ProjectWarehouseDto>>> Handle(GetProjectWarehousesQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdWithInclude(request.projectId, w => w.ProjectWarehouses);
            if (project is  null) 
            return GeneralResponse<List<ProjectWarehouseDto>>.Fail($"project with id {request.projectId} not found");

            var projectWarehouseDto = _mapper.Map<List<ProjectWarehouseDto>>(project.ProjectWarehouses);

            return GeneralResponse<List<ProjectWarehouseDto>>.Success(projectWarehouseDto);
        }
    }
}
