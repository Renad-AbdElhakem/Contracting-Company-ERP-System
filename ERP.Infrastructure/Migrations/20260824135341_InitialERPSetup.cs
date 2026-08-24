using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialERPSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacitySquareMeters = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsFull = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWarehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractSigningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpectedEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualEndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TerminationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufactureYear = table.Column<int>(type: "int", nullable: false),
                    EquipmentType = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EquipmentStatus = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CompanyWarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_CompanyWarehouses_CompanyWarehouseId",
                        column: x => x.CompanyWarehouseId,
                        principalTable: "CompanyWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipment_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPurchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ArrivalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPurchases_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPurchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ArrivalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialPurchases_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierMaterialPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierMaterialPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierMaterialPrices_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierMaterialPrices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SignDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PenaltyClauseNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PenaltyPerDay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MaxPenaltyAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractProjects_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FinishedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EstimatedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ActualCostCalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPhases_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPhases_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacitySquareMeters = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWarehouses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyWarehouseStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArrivalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyWarehouseId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedByEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyWarehouseStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyWarehouseStocks_CompanyWarehouses_CompanyWarehouseId",
                        column: x => x.CompanyWarehouseId,
                        principalTable: "CompanyWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyWarehouseStocks_Employees_ReceivedByEmployeeId",
                        column: x => x.ReceivedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyWarehouseStocks_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEmployees_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFinancialSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SnapshotDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ActualRevenue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ActualCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ActualProfitLoss = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFinancialSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFinancialSnapshots_Employees_CreatedByEmployeeId",
                        column: x => x.CreatedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectFinancialSnapshots_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOrderRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOrderRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOrderRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectOrderRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipment_Maintenances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_Maintenances_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectEquipment_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPurchaseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EquipmentPurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPurchaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPurchaseItems_EquipmentPurchases_EquipmentPurchaseId",
                        column: x => x.EquipmentPurchaseId,
                        principalTable: "EquipmentPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentPurchaseItems_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentPurchasePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaidDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EquipmentPurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPurchasePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPurchasePayments_EquipmentPurchases_EquipmentPurchaseId",
                        column: x => x.EquipmentPurchaseId,
                        principalTable: "EquipmentPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPurchaseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaterialPurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPurchaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialPurchaseItems_MaterialPurchases_MaterialPurchaseId",
                        column: x => x.MaterialPurchaseId,
                        principalTable: "MaterialPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialPurchaseItems_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPurchasePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaidDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MaterialPurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPurchasePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialPurchasePayments_MaterialPurchases_MaterialPurchaseId",
                        column: x => x.MaterialPurchaseId,
                        principalTable: "MaterialPurchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractInstallmentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthsAfterSignDate = table.Column<int>(type: "int", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInstallmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractInstallmentPlans_ContractProjects_ContractId",
                        column: x => x.ContractId,
                        principalTable: "ContractProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractPaymentRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PaidDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPaymentRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractPaymentRecords_ContractProjects_ContractId",
                        column: x => x.ContractId,
                        principalTable: "ContractProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhaseMaterialRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectPhaseId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseMaterialRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhaseMaterialRequirements_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhaseMaterialRequirements_ProjectPhases_ProjectPhaseId",
                        column: x => x.ProjectPhaseId,
                        principalTable: "ProjectPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectExpenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectPhaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectExpenses_ProjectPhases_ProjectPhaseId",
                        column: x => x.ProjectPhaseId,
                        principalTable: "ProjectPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWarehouseStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectWarehouseId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedByEmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWarehouseStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWarehouseStocks_Employees_ReceivedByEmployeeId",
                        column: x => x.ReceivedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectWarehouseStocks_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectWarehouseStocks_ProjectWarehouses_ProjectWarehouseId",
                        column: x => x.ProjectWarehouseId,
                        principalTable: "ProjectWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId1 = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderMaterials_ProjectOrderRequests_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "ProjectOrderRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Equipment_MaintenanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceEmployees_Equipment_Maintenances_Equipment_MaintenanceId",
                        column: x => x.Equipment_MaintenanceId,
                        principalTable: "Equipment_Maintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialConsumptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityUsed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectWarehouseStockId = table.Column<int>(type: "int", nullable: false),
                    ProjectPhaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialConsumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialConsumptions_ProjectPhases_ProjectPhaseId",
                        column: x => x.ProjectPhaseId,
                        principalTable: "ProjectPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialConsumptions_ProjectWarehouseStocks_ProjectWarehouseStockId",
                        column: x => x.ProjectWarehouseStockId,
                        principalTable: "ProjectWarehouseStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWarehouseStocks_CompanyWarehouseId",
                table: "CompanyWarehouseStocks",
                column: "CompanyWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWarehouseStocks_MaterialId",
                table: "CompanyWarehouseStocks",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWarehouseStocks_ReceivedByEmployeeId",
                table: "CompanyWarehouseStocks",
                column: "ReceivedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractInstallmentPlans_ContractId",
                table: "ContractInstallmentPlans",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPaymentRecords_ContractId",
                table: "ContractPaymentRecords",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractProjects_ClientId",
                table: "ContractProjects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractProjects_ProjectId",
                table: "ContractProjects",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CompanyWarehouseId",
                table: "Equipment",
                column: "CompanyWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SupplierId",
                table: "Equipment",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Maintenances_EquipmentId",
                table: "Equipment_Maintenances",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchaseItems_EquipmentId",
                table: "EquipmentPurchaseItems",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchaseItems_EquipmentPurchaseId",
                table: "EquipmentPurchaseItems",
                column: "EquipmentPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchasePayments_DueDate",
                table: "EquipmentPurchasePayments",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchasePayments_EquipmentPurchaseId",
                table: "EquipmentPurchasePayments",
                column: "EquipmentPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchases_SupplierId",
                table: "EquipmentPurchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceEmployees_EmployeeId",
                table: "MaintenanceEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceEmployees_Equipment_MaintenanceId",
                table: "MaintenanceEmployees",
                column: "Equipment_MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_ProjectPhaseId",
                table: "MaterialConsumptions",
                column: "ProjectPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_ProjectWarehouseStockId",
                table: "MaterialConsumptions",
                column: "ProjectWarehouseStockId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchaseItems_MaterialId",
                table: "MaterialPurchaseItems",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchaseItems_MaterialPurchaseId",
                table: "MaterialPurchaseItems",
                column: "MaterialPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchasePayments_DueDate",
                table: "MaterialPurchasePayments",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchasePayments_MaterialPurchaseId",
                table: "MaterialPurchasePayments",
                column: "MaterialPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPurchases_SupplierId",
                table: "MaterialPurchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterials_MaterialId",
                table: "OrderMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterials_OrderId1",
                table: "OrderMaterials",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseMaterialRequirements_MaterialId",
                table: "PhaseMaterialRequirements",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseMaterialRequirements_ProjectPhaseId",
                table: "PhaseMaterialRequirements",
                column: "ProjectPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_EmployeeId",
                table: "ProjectEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEmployees_ProjectId",
                table: "ProjectEmployees",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEquipment_EquipmentId",
                table: "ProjectEquipment",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEquipment_ProjectId",
                table: "ProjectEquipment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectExpenses_ProjectPhaseId",
                table: "ProjectExpenses",
                column: "ProjectPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFinancialSnapshots_CreatedByEmployeeId",
                table: "ProjectFinancialSnapshots",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFinancialSnapshots_ProjectId",
                table: "ProjectFinancialSnapshots",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOrderRequests_EmployeeId",
                table: "ProjectOrderRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOrderRequests_ProjectId",
                table: "ProjectOrderRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhases_PhaseId",
                table: "ProjectPhases",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhases_ProjectId",
                table: "ProjectPhases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouses_ProjectId",
                table: "ProjectWarehouses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouseStocks_MaterialId",
                table: "ProjectWarehouseStocks",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouseStocks_ProjectWarehouseId",
                table: "ProjectWarehouseStocks",
                column: "ProjectWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouseStocks_ReceivedByEmployeeId",
                table: "ProjectWarehouseStocks",
                column: "ReceivedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierMaterialPrices_MaterialId",
                table: "SupplierMaterialPrices",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierMaterialPrices_SupplierId",
                table: "SupplierMaterialPrices",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyWarehouseStocks");

            migrationBuilder.DropTable(
                name: "ContractInstallmentPlans");

            migrationBuilder.DropTable(
                name: "ContractPaymentRecords");

            migrationBuilder.DropTable(
                name: "EquipmentPurchaseItems");

            migrationBuilder.DropTable(
                name: "EquipmentPurchasePayments");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "MaintenanceEmployees");

            migrationBuilder.DropTable(
                name: "MaterialConsumptions");

            migrationBuilder.DropTable(
                name: "MaterialPurchaseItems");

            migrationBuilder.DropTable(
                name: "MaterialPurchasePayments");

            migrationBuilder.DropTable(
                name: "OrderMaterials");

            migrationBuilder.DropTable(
                name: "PhaseMaterialRequirements");

            migrationBuilder.DropTable(
                name: "ProjectEmployees");

            migrationBuilder.DropTable(
                name: "ProjectEquipment");

            migrationBuilder.DropTable(
                name: "ProjectExpenses");

            migrationBuilder.DropTable(
                name: "ProjectFinancialSnapshots");

            migrationBuilder.DropTable(
                name: "SupplierMaterialPrices");

            migrationBuilder.DropTable(
                name: "ContractProjects");

            migrationBuilder.DropTable(
                name: "EquipmentPurchases");

            migrationBuilder.DropTable(
                name: "Equipment_Maintenances");

            migrationBuilder.DropTable(
                name: "ProjectWarehouseStocks");

            migrationBuilder.DropTable(
                name: "MaterialPurchases");

            migrationBuilder.DropTable(
                name: "ProjectOrderRequests");

            migrationBuilder.DropTable(
                name: "ProjectPhases");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "ProjectWarehouses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "CompanyWarehouses");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
