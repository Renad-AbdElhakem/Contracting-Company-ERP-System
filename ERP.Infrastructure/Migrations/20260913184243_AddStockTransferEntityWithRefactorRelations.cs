using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockTransferEntityWithRefactorRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWarehouseStocks_CompanyWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropColumn(
                name: "SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.AddColumn<int>(
                name: "StockTransferId",
                table: "ProjectWarehouseStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StockTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyWarehouseStockId = table.Column<int>(type: "int", nullable: false),
                    ProjectWarehouseId = table.Column<int>(type: "int", nullable: false),
                    OrderMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransfers_CompanyWarehouseStocks_CompanyWarehouseStockId",
                        column: x => x.CompanyWarehouseStockId,
                        principalTable: "CompanyWarehouseStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransfers_OrderMaterials_OrderMaterialId",
                        column: x => x.OrderMaterialId,
                        principalTable: "OrderMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransfers_ProjectWarehouses_ProjectWarehouseId",
                        column: x => x.ProjectWarehouseId,
                        principalTable: "ProjectWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouseStocks_StockTransferId",
                table: "ProjectWarehouseStocks",
                column: "StockTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_CompanyWarehouseStockId",
                table: "StockTransfers",
                column: "CompanyWarehouseStockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_OrderMaterialId",
                table: "StockTransfers",
                column: "OrderMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_ProjectWarehouseId",
                table: "StockTransfers",
                column: "ProjectWarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWarehouseStocks_StockTransfers_StockTransferId",
                table: "ProjectWarehouseStocks",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWarehouseStocks_StockTransfers_StockTransferId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropTable(
                name: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectWarehouseStocks_StockTransferId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropColumn(
                name: "StockTransferId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.AddColumn<int>(
                name: "SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks",
                column: "SourceCompanyWarehouseStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWarehouseStocks_CompanyWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks",
                column: "SourceCompanyWarehouseStockId",
                principalTable: "CompanyWarehouseStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
