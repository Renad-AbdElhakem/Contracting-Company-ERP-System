using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Command;
using ERP.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialHandler.CommandHandler
{
    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, GeneralResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IMaterialRepository _materialRepository;

        public UpdateMaterialCommandHandler(IMapper mapper, IMaterialRepository materialRepository)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
        }

        public async Task<GeneralResponse<bool>> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetByIdAsync(request.MaterialId);

            if (material is null)
                return GeneralResponse<bool>.Fail($"Material with id {request.MaterialId} not found");

            if (!string.IsNullOrEmpty(request.MaterialDto.Name))
                material.Name = request.MaterialDto.Name;

            if (!string.IsNullOrEmpty(request.MaterialDto.Unit))
                material.Unit = request.MaterialDto.Unit;


            if (request.MaterialDto.MaterialType is not null)
                material.MaterialType = (MaterialType)request.MaterialDto.MaterialType;


            await _materialRepository.UpdateAsync(material);

            return GeneralResponse<bool>.Success(true);
        }
    }
}

