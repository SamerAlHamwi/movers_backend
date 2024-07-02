using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationShipBetweenCompanyBranchAndServiceValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_ServiceValues_ServiceValueId",
                table: "CompanyBranches");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBranches_ServiceValueId",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "ServiceValueId",
                table: "CompanyBranches");

            migrationBuilder.AddColumn<int>(
                name: "CompanyBranchId",
                table: "ServiceValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_CompanyBranchId",
                table: "ServiceValues",
                column: "CompanyBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_CompanyBranches_CompanyBranchId",
                table: "ServiceValues",
                column: "CompanyBranchId",
                principalTable: "CompanyBranches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_CompanyBranches_CompanyBranchId",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceValues_CompanyBranchId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "CompanyBranchId",
                table: "ServiceValues");

            migrationBuilder.AddColumn<long>(
                name: "ServiceValueId",
                table: "CompanyBranches",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranches_ServiceValueId",
                table: "CompanyBranches",
                column: "ServiceValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_ServiceValues_ServiceValueId",
                table: "CompanyBranches",
                column: "ServiceValueId",
                principalTable: "ServiceValues",
                principalColumn: "Id");
        }
    }
}
