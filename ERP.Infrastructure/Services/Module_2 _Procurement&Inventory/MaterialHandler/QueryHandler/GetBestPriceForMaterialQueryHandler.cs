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
    public class GetBestPriceForMaterialQueryHandler : IRequestHandler<GetBestPriceForMaterialQuery, GeneralResponse<SupplierMaterialPriceDto>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly ISupplierMaterialPriceRepository _materialPriceRepository;
        private readonly IMapper _mapper;

        public GetBestPriceForMaterialQueryHandler(IMaterialRepository materialRepository, ISupplierMaterialPriceRepository materialPriceRepository
                                                    , IMapper mapper)
        {
            _materialRepository = materialRepository;
            _materialPriceRepository = materialPriceRepository;
            _mapper = mapper;
        }
        public async Task<GeneralResponse<SupplierMaterialPriceDto>> Handle(GetBestPriceForMaterialQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetByIdAsync(request.materialId);

            if (material == null)
                return GeneralResponse<SupplierMaterialPriceDto>.Fail("Material not found");


            var materialprice = await _materialPriceRepository.BestMaterialpriceAsync(request.materialId);

            var materialPriceDto = _mapper.Map<SupplierMaterialPriceDto>(materialprice);

            return GeneralResponse<SupplierMaterialPriceDto>.Success(materialPriceDto);
        }
    }
}
