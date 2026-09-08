using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceivingStatus",
                table: "ProjectWarehouseStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialPurchaseItemId",
                table: "CompanyWarehouseStocks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceivingStatus",
                table: "CompanyWarehouseStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks",
                column: "SourceCompanyWarehouseStockId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyWarehouseStocks_MaterialPurchaseItemId",
                table: "CompanyWarehouseStocks",
                column: "MaterialPurchaseItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyWarehouseStocks_MaterialPurchaseItems_MaterialPurchaseItemId",
                table: "CompanyWarehouseStocks",
                column: "MaterialPurchaseItemId",
                principalTable: "MaterialPurchaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWarehouseStocks_CompanyWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks",
                column: "SourceCompanyWarehouseStockId",
                principalTable: "CompanyWarehouseStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyWarehouseStocks_MaterialPurchaseItems_MaterialPurchaseItemId",
                table: "CompanyWarehouseStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWarehouseStocks_CompanyWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectWarehouseStocks_SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropIndex(
                name: "IX_CompanyWarehouseStocks_MaterialPurchaseItemId",
                table: "CompanyWarehouseStocks");

            migrationBuilder.DropColumn(
                name: "ReceivingStatus",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropColumn(
                name: "SourceCompanyWarehouseStockId",
                table: "ProjectWarehouseStocks");

            migrationBuilder.DropColumn(
                name: "MaterialPurchaseItemId",
                table: "CompanyWarehouseStocks");

            migrationBuilder.DropColumn(
                name: "ReceivingStatus",
                table: "CompanyWarehouseStocks");
        }
    }
}
