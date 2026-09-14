using ERP.Domain.Model;
using ERP.Domain.Model._1_Project_Site_Management;
using ERP.Domain.Model.Module_1_Project_Site_Management;
using ERP.Domain.Model.Module_2___Procurement___Inventory;
using ERP.Domain.Model.Module_2__Procurement_Inventory;
using ERP.Domain.Model.Module_3___Equipment_Machinery;
using ERP.Domain.Model.Module_4___Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        // Module 1 — Project & Site Management
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<ProjectPhase> ProjectPhases { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ContractProject> ContractProjects { get; set; }
        public DbSet<ContractInstallmentPlan> ContractInstallmentPlans { get; set; }
        public DbSet<ContractPaymentRecord> ContractPaymentRecords { get; set; }

        // Module 2 — Procurement & Inventory
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierMaterialPrice> SupplierMaterialPrices { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<CompanyWarehouse> CompanyWarehouses { get; set; }
        public DbSet<CompanyWarehouseStock> CompanyWarehouseStocks { get; set; }
        public DbSet<ProjectWarehouse> ProjectWarehouses { get; set; }
        public DbSet<ProjectWarehouseStock> ProjectWarehouseStocks { get; set; }
        public DbSet<PhaseMaterialRequirement> PhaseMaterialRequirements { get; set; }
        public DbSet<MaterialConsumption> MaterialConsumptions { get; set; }
        public DbSet<ProjectOrderRequest> ProjectOrderRequests { get; set; }
        public DbSet<OrderMaterials> OrderMaterials { get; set; }
        public DbSet<StockTransfer>   StockTransfers { get; set; }

        // Module 3 — Equipment / Machinery
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Equipment_Maintenance> Equipment_Maintenances { get; set; }
        public DbSet<MaintenanceEmployee> MaintenanceEmployees { get; set; }
        public DbSet<ProjectEquipment> ProjectEquipment { get; set; }


        // Module 4 — Finance
        public DbSet<MaterialPurchase> MaterialPurchases { get; set; }
        public DbSet<MaterialPurchaseItem> MaterialPurchaseItems { get; set; }
        public DbSet<MaterialPurchasePayment> MaterialPurchasePayments { get; set; }
        public DbSet<EquipmentPurchase> EquipmentPurchases { get; set; }
        public DbSet<EquipmentPurchaseItem> EquipmentPurchaseItems { get; set; }
        public DbSet<EquipmentPurchasePayment> EquipmentPurchasePayments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ProjectExpense> ProjectExpenses { get; set; }
        public DbSet<ProjectFinancialSnapshot> ProjectFinancialSnapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }


    }
}
