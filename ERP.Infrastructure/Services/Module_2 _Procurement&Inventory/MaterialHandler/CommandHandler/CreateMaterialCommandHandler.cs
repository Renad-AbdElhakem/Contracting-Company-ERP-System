using AutoMapper;
using ERP.Application;
using ERP.Application.Interfaces.Repository.Module_2__Procurement_Inventory;
using ERP.Application.Interfaces.Services.Module_2__Procurement_Inventory.MaterialService.Command;
using ERP.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_2__Procurement_Inventory.MaterialHandler.CommandHandler
{
    public class CreateMaterialCommandHandler
    : IRequestHandler<CreateMaterialCommand, GeneralResponse<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IMaterialRepository _materialRepository;

        public CreateMaterialCommandHandler(IMapper mapper, IMaterialRepository materialRepository)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
        }

        public async Task<GeneralResponse<Guid>> Handle(CreateMaterialCommand request,CancellationToken cancellationToken)
        {
            if (request.NewMaterialDto is null)
                return GeneralResponse<Guid>.Fail("Must have data");

            var material = _mapper.Map<Material>(request.NewMaterialDto);

            await _materialRepository.AddAsync(material);

            return GeneralResponse<Guid>.Success(material.Id);
        }
    }
}
