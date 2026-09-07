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
    public class GetAllMaterialsByFilterQueryHandler: IRequestHandler<GetAllMaterialsByFilterQuery, GeneralResponse<List<MaterialDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IMaterialRepository _materialRepository;

        public GetAllMaterialsByFilterQueryHandler(IMapper mapper, IMaterialRepository materialRepository)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
        }

        public async Task<GeneralResponse<List<MaterialDto>>> Handle(GetAllMaterialsByFilterQuery request, CancellationToken cancellationToken)
        {
            var materials = await _materialRepository.GetAllAsync();

            if (request.Type.HasValue)
                materials = materials
                    .Where(x => x.MaterialType == request.Type.Value)
                    .ToList();

            if (!string.IsNullOrWhiteSpace(request.Unit))
                materials = materials
                    .Where(x => x.Unit.Contains(
                        request.Unit,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();

            var result = _mapper.Map<List<MaterialDto>>(materials);

            return GeneralResponse<List<MaterialDto>>.Success(result);
        }
    }
}
