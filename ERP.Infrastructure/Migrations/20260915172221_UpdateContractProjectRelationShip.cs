using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContractProjectRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractInstallmentPlanId",
                table: "ContractPaymentRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContractPaymentRecords_ContractInstallmentPlanId",
                table: "ContractPaymentRecords",
                column: "ContractInstallmentPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractPaymentRecords_ContractInstallmentPlans_ContractInstallmentPlanId",
                table: "ContractPaymentRecords",
                column: "ContractInstallmentPlanId",
                principalTable: "ContractInstallmentPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractPaymentRecords_ContractInstallmentPlans_ContractInstallmentPlanId",
                table: "ContractPaymentRecords");

            migrationBuilder.DropIndex(
                name: "IX_ContractPaymentRecords_ContractInstallmentPlanId",
                table: "ContractPaymentRecords");

            migrationBuilder.DropColumn(
                name: "ContractInstallmentPlanId",
                table: "ContractPaymentRecords");
        }
    }
}
