using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class FixCompanyContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContacts_Companies_CompanyId",
                table: "CompanyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContacts_CompanyBranches_CompanyBranchId",
                table: "CompanyContacts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContacts_CompanyBranchId",
                table: "CompanyContacts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts");

            migrationBuilder.DropColumn(
                name: "CompanyBranchId",
                table: "CompanyContacts");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyContacts");

            migrationBuilder.DropColumn(
                name: "IsForBranchCompany",
                table: "CompanyContacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyBranchId",
                table: "CompanyContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "CompanyContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForBranchCompany",
                table: "CompanyContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyBranchId",
                table: "CompanyContacts",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContacts_Companies_CompanyId",
                table: "CompanyContacts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContacts_CompanyBranches_CompanyBranchId",
                table: "CompanyContacts",
                column: "CompanyBranchId",
                principalTable: "CompanyBranches",
                principalColumn: "Id");
        }
    }
}
