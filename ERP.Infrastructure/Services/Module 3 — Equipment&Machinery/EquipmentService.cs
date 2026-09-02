using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Repository.Module_3___Equipment_Machinery;
using ERP.Application.Interfaces.Services.Module_3___Equipment_Machinery;
using ERP.Domain.Enum;
using ERP.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_3___Equipment_Machinery
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IMapper _mapper;
        private readonly IMaintenanceService _maintenanceService;

        public EquipmentService(IEquipmentRepository equipmentRepository, IMapper mapper, IMaintenanceService maintenanceService)
        {
            _equipmentRepository = equipmentRepository;
            _mapper = mapper;
            _maintenanceService = maintenanceService;
        }

        public async Task<List<EquipmentsDto>> GetEquipments()
        {
            var equipments =  await _equipmentRepository.GetAllAsync(s=>s.Supplier);
            return _mapper.Map<List<EquipmentsDto>>(equipments);
        }

        public async Task<List<EquipmentsDto>> GetEquipmentsByFilter(GetEquipmentsWithFilterDto withFilterDto)
        {

            var equipments = _equipmentRepository.GetEquipmentsByFilterAsync();

            if (!string.IsNullOrEmpty(withFilterDto.Name))
                equipments = equipments.Where(e => e.Name == withFilterDto.Name);

            if (!string.IsNullOrEmpty(withFilterDto.Brand))
                equipments = equipments.Where(e => e.Brand == withFilterDto.Brand);

            if (withFilterDto.EquipmentStatus is not null)
                equipments = equipments.Where(e => e.EquipmentStatus == withFilterDto.EquipmentStatus);

            if (withFilterDto.ManufactureYear.HasValue)
                equipments = equipments.Where(e => e.ManufactureYear == withFilterDto.ManufactureYear);

            if (withFilterDto.SupplierId.HasValue)
                equipments = equipments.Where(e => e.SupplierId == withFilterDto.SupplierId);

            if (withFilterDto.CompanyWarehouseId.HasValue)
                equipments = equipments.Where(e => e.CompanyWarehouseId == withFilterDto.CompanyWarehouseId);


            var result = await equipments.ToListAsync();

            return _mapper.Map<List<EquipmentsDto>>(result);

        }

        public async Task<GeneralResponse<EquipmentsDto>> GetEquipmentById(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

            if (equipment == null)
                return GeneralResponse<EquipmentsDto>.Fail($"Equipment with id {equipmentId} not found");

            var equipmentDto = _mapper.Map<EquipmentsDto>(equipment);
            return GeneralResponse<EquipmentsDto>.Success(equipmentDto);

        }

        public async Task<GeneralResponse<Guid>> AddEquipmentsAsync(AddNewEquipmentDto dto)
        {
            //R(suplier)

            if (dto == null)
                return GeneralResponse<Guid>.Fail("NULL");

            var equipment = _mapper.Map<Equipment>(dto);

            await _equipmentRepository.AddAsync(equipment);
            return GeneralResponse<Guid>.Success(equipment.Id);
        }

        public async Task<GeneralResponse<bool>> SoftDeleteAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

            if (equipment == null)
                return GeneralResponse<bool>.Fail($"Equipment with id {equipmentId} not found");

            equipment.EquipmentStatus = EquipmentStatus.OutOfService;

            await _equipmentRepository.UpdateAsync(equipment);

            return GeneralResponse<bool>.Success(true);
        }
        public async Task<GeneralResponse<bool>> UpdateAsync(Guid equipmentId, UpdateEquipmentDto dto)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

            if (equipment == null)
                return GeneralResponse<bool>.Fail($"Equipment with id {equipmentId} not found");

            if (dto.Name != null)
                equipment.Name = dto.Name;

            if (dto.Brand != null)
                equipment.Brand = dto.Brand;

            if (dto.ManufactureYear.HasValue)
                equipment.ManufactureYear = dto.ManufactureYear.Value;

            if (dto.EquipmentType.HasValue)
                equipment.EquipmentType = dto.EquipmentType.Value;

            if (dto.PurchaseDate.HasValue)
                equipment.PurchaseDate = dto.PurchaseDate.Value;

            await _equipmentRepository.UpdateAsync(equipment);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<Guid>> AddMaintenanceAsync(Guid equipmentId, AddMaintenanceDto dto)
        {
            if (dto == null)
                return GeneralResponse<Guid>.Fail("Maintenance data is required.");

            var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

            if (equipment == null)
                return GeneralResponse<Guid>.Fail($"Equipment with id {equipmentId} not found.");

            if (equipment.EquipmentStatus != EquipmentStatus.Available)
                return GeneralResponse<Guid>.Fail($"Equipment is not available. Current status: {equipment.EquipmentStatus}.");


            var maintenance = await _maintenanceService.AddNewMaintenance(equipmentId, dto);

            if (!maintenance.IsSuccess)
                return GeneralResponse<Guid>.Fail(maintenance.Message);

            equipment.EquipmentStatus = EquipmentStatus.UnderMaintenance;
            await _equipmentRepository.UpdateAsync(equipment);

            return GeneralResponse<Guid>.Success(maintenance.Data);
        }


        public async Task<GeneralResponse<List<MaintenanceDto>>> GetMaintenceHistoryByEquipmentId(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetByIdWithInclude(equipmentId, m => m.Equipment_Maintenances);

            if (equipment is null)
                return GeneralResponse<List<MaintenanceDto>>.Fail($"Equipment with id {equipmentId} not found");

            var maintenanceDto = _mapper.Map<List<MaintenanceDto>>(equipment.Equipment_Maintenances);

            return GeneralResponse<List<MaintenanceDto>>.Success(maintenanceDto);

        }
        public async Task<GeneralResponse<List<ProjectEquipmentDto>>> GetProjectsHistoryByEquipmentId(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetByIdWithInclude(equipmentId, m => m.ProjectEquipment);

            if (equipment is null)
                return GeneralResponse<List<ProjectEquipmentDto>>.Fail($"Equipment with id {equipmentId} not found");

            var projectEquipmentDto = _mapper.Map<List<ProjectEquipmentDto>>(equipment.ProjectEquipment);

            return GeneralResponse<List<ProjectEquipmentDto>>.Success(projectEquipmentDto);

        }

        public async Task<GeneralResponse<bool>> MarkAsAvailableAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

            if (equipment == null)
                return GeneralResponse<bool>.Fail($"Equipment with id {equipmentId} not found");

            equipment.EquipmentStatus = EquipmentStatus.Available;

            await _equipmentRepository.UpdateAsync(equipment);

            return GeneralResponse<bool>.Success(true);
        }
        public async Task<GeneralResponse<bool>> MarkAsAvailableAsync(List<Guid> equipmentId)
        {

            var equipments = await _equipmentRepository.GetAllEquipmentByIds(equipmentId);

            if (!equipments.Any())
                return GeneralResponse<bool>.Fail($"Equipment with id {equipmentId} not found");

            equipments.ForEach(x => x.EquipmentStatus = EquipmentStatus.Available);

            await _equipmentRepository.UpdateRangeAsync(equipments);

            return GeneralResponse<bool>.Success(true);
        }

        public async Task<GeneralResponse<bool>> MarkAsInUseAsync(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetByIdAsync(equipmentId);

            if (equipment == null)
                return GeneralResponse<bool>.Fail($"Equipment with id {equipmentId} not found");

            equipment.EquipmentStatus = EquipmentStatus.InUse;

            await _equipmentRepository.UpdateAsync(equipment);

            return GeneralResponse<bool>.Success(true);
        }


    }
}
