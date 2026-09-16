using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.MaterialPurchaseDtos;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.MaterialPurchaseService.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.MaterialPurchaseHandler.QueryHandler
{
    public class GetMaterialPurchaseByIdQueryHandler
     : IRequestHandler<GetMaterialPurchaseByIdQuery, GeneralResponse<MaterialPurchaseDto>>
    {
        private readonly IMaterialPurchaseRepository _materialPurchaseRepository;
        private readonly IMapper _mapper;

        public GetMaterialPurchaseByIdQueryHandler(IMaterialPurchaseRepository materialPurchaseRepository, IMapper mapper)
        {
            _materialPurchaseRepository = materialPurchaseRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<MaterialPurchaseDto>> Handle( GetMaterialPurchaseByIdQuery request, CancellationToken cancellationToken)
        {
            var purchase = await _materialPurchaseRepository.GetMaterialPurchaseDetailsById(request.materialPurchaseId);

            if (purchase is null)
                return GeneralResponse<MaterialPurchaseDto>.Fail("Material purchase not found.");

            var purchaseDto = _mapper.Map<MaterialPurchaseDto>(purchase);

            return GeneralResponse<MaterialPurchaseDto>.Success(purchaseDto);
        }
    }
}
