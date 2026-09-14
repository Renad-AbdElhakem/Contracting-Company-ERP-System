using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEmployeeProjectOrderRequesrRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOrderRequests_Employees_EmployeeId",
                table: "ProjectOrderRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectOrderRequests_EmployeeId",
                table: "ProjectOrderRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ProjectOrderRequests");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOrderRequests_RequestByEmployeeId",
                table: "ProjectOrderRequests",
                column: "RequestByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOrderRequests_Employees_RequestByEmployeeId",
                table: "ProjectOrderRequests",
                column: "RequestByEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOrderRequests_Employees_RequestByEmployeeId",
                table: "ProjectOrderRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectOrderRequests_RequestByEmployeeId",
                table: "ProjectOrderRequests");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ProjectOrderRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOrderRequests_EmployeeId",
                table: "ProjectOrderRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOrderRequests_Employees_EmployeeId",
                table: "ProjectOrderRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
