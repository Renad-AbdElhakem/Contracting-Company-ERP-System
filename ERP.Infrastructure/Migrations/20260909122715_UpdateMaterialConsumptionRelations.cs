using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaterialConsumptionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MaterialId",
                table: "MaterialConsumptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MaterialConsumptions_MaterialId",
                table: "MaterialConsumptions",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialConsumptions_Materials_MaterialId",
                table: "MaterialConsumptions",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialConsumptions_Materials_MaterialId",
                table: "MaterialConsumptions");

            migrationBuilder.DropIndex(
                name: "IX_MaterialConsumptions_MaterialId",
                table: "MaterialConsumptions");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "MaterialConsumptions");
        }
    }
}
