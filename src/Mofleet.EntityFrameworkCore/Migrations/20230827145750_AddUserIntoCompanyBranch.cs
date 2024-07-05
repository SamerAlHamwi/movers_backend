using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIntoCompanyBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "CompanyBranches",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyContactId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranches_UserId",
                table: "CompanyBranches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyContactId",
                table: "Companies",
                column: "CompanyContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyContacts_CompanyContactId",
                table: "Companies",
                column: "CompanyContactId",
                principalTable: "CompanyContacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_AbpUsers_UserId",
                table: "CompanyBranches",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyContacts_CompanyContactId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_AbpUsers_UserId",
                table: "CompanyBranches");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBranches_UserId",
                table: "CompanyBranches");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CompanyContactId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "CompanyContactId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");
        }
    }
}
