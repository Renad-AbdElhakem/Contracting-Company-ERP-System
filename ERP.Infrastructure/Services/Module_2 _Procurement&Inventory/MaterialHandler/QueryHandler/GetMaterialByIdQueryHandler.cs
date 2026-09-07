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
    public class GetMaterialByIdQueryHandler: IRequestHandler<GetMaterialByIdQuery, GeneralResponse<MaterialDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMaterialRepository _materialRepository;

        public GetMaterialByIdQueryHandler(IMapper mapper, IMaterialRepository materialRepository)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
        }

        public async Task<GeneralResponse<MaterialDto>> Handle( GetMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetByIdAsync(request.materialId);

            if (material == null)
                return GeneralResponse<MaterialDto>.Fail( $"Material with id {request.materialId} not found");

            var dto = _mapper.Map<MaterialDto>(material);

            return GeneralResponse<MaterialDto>.Success(dto);
        }
    }
}
