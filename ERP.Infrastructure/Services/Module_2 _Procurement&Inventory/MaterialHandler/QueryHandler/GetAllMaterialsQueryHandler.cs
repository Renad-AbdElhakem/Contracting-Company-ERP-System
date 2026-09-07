using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_2__Procurement_Inventory.MaterialDtos;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialHandler.QueryHandler
{
    public class GetAllMaterialsQueryHandler : IRequestHandler<GetAllMaterialsQuery, GeneralResponse<List<MaterialDto>>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetAllMaterialsQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<List<MaterialDto>>> Handle(GetAllMaterialsQuery request, CancellationToken cancellationToken)
        {
            var materials = await _materialRepository.GetAllAsync();
            var materialsDto = _mapper.Map<List<MaterialDto>>(materials);
            return GeneralResponse<List<MaterialDto>>.Success(materialsDto);
        }
    }
}
