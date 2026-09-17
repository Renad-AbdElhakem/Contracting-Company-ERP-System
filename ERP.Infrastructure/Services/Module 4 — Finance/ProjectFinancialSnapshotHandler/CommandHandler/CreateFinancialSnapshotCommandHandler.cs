using AutoMapper;
using ERP.Application;
using ERP.Application.Dtos.Module_4___Finance.ProjectFinancialSnapshotDtos;
using ERP.Application.Interfaces.Repository;
using ERP.Application.Interfaces.Repository.Module_1_Project_Site_Management;
using ERP.Application.Interfaces.Repository.Module_4___Finance;
using ERP.Application.Interfaces.Services.Module_4___Finance.ContractService.Query;
using ERP.Application.Interfaces.Services.Module_4___Finance.ProjectFinancialSnapshotService.Command;
using ERP.Domain.Model;
using ERP.Domain.Model.Module_4___Finance;
using ERP.Infrastructure.Repository.Module_4___Finance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Services.Module_4___Finance.ProjectFinancialSnapshotHandler.CommandHandler
{
 
    public class CreateFinancialSnapshotCommandHandler
        : IRequestHandler<CreateFinancialSnapshotCommand, GeneralResponse<Guid>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IContractProjectRepository _contractProjectRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IMaterialPurchaseRepository _materialPurchaseRepository;
        private readonly IProjectFinancialSnapshotRepository _projectFinancialSnapshotRepository;
        private readonly IMapper _mapper;

        public CreateFinancialSnapshotCommandHandler(
            IProjectRepository projectRepository,
            IContractProjectRepository contractProjectRepository,
            IProjectPhaseRepository projectPhaseRepository,
            IMaterialPurchaseRepository materialPurchaseRepository,
            IProjectFinancialSnapshotRepository projectFinancialSnapshotRepository,
            IMapper mapper)
        {
            _projectRepository = projectRepository;
            _contractProjectRepository = contractProjectRepository;
            _projectPhaseRepository = projectPhaseRepository;
            _materialPurchaseRepository = materialPurchaseRepository;
            _projectFinancialSnapshotRepository = projectFinancialSnapshotRepository;
            _mapper = mapper;
        }

        public async Task<GeneralResponse<Guid>> Handle(CreateFinancialSnapshotCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);
            if (project is null)
                return GeneralResponse<Guid>.Fail($"Project with id {request.ProjectId} not found");

            // 1) Revenue
            var actualRevenue = await _contractProjectRepository.GetSumContractPaymentRecordsByProjectId(request.ProjectId) ?? 0;

            // 2) Cost - Part A: manual expenses across all phases
            var expensesTotal = await _projectPhaseRepository.GetTotalExpensesByProjectIdAsync(request.ProjectId);

            // 2) Cost - Part B: material consumption cost across all phases
            var consumptions = await _projectPhaseRepository.GetMaterialConsumptionsByProjectIdAsync(request.ProjectId);

            var materialIds = consumptions.Select(c => c.MaterialId).Distinct().ToList();

            var purchaseItems = await _materialPurchaseRepository.GetMaterialPurchaseItemsByMaterialIds(materialIds);

            var averagePricesByMaterial = purchaseItems
                .GroupBy(item => item.MaterialId)
                .ToDictionary(g => g.Key, g => g.Average(item => item.UnitPrice));

            decimal materialsCost = 0;
            foreach (var consumption in consumptions)
            {
                if (averagePricesByMaterial.TryGetValue(consumption.MaterialId, out var avgUnitPrice))
                    materialsCost += avgUnitPrice * consumption.QuantityUsed;
            }

            var actualCost = expensesTotal + materialsCost;
            var actualProfitLoss = actualRevenue - actualCost;

            var snapshot = new ProjectFinancialSnapshot
            {
                ProjectId = request.ProjectId,
                SnapshotDate = DateOnly.FromDateTime(DateTime.Now),
                ActualRevenue = actualRevenue,
                ActualCost = actualCost,
                ActualProfitLoss = actualProfitLoss,
                CreatedByEmployeeId = request.CreatedByEmployeeId
            };

            await _projectFinancialSnapshotRepository.AddAsync(snapshot);

            return GeneralResponse<Guid>.Success(snapshot.Id);
        }
    }
}
