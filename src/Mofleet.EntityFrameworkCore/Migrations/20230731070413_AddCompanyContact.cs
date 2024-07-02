using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_CompanyContact_CompanyContactId",
                table: "CompanyBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_Companies_CompanyId",
                table: "CompanyContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_CompanyBranches_CompanyBranchId",
                table: "CompanyContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyContact",
                table: "CompanyContact");

            migrationBuilder.RenameTable(
                name: "CompanyContact",
                newName: "CompanyContacts");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyContact_CompanyId",
                table: "CompanyContacts",
                newName: "IX_CompanyContacts_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyContact_CompanyBranchId",
                table: "CompanyContacts",
                newName: "IX_CompanyContacts_CompanyBranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyContacts",
                table: "CompanyContacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_CompanyContacts_CompanyContactId",
                table: "CompanyBranches",
                column: "CompanyContactId",
                principalTable: "CompanyContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_CompanyContacts_CompanyContactId",
                table: "CompanyBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContacts_Companies_CompanyId",
                table: "CompanyContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContacts_CompanyBranches_CompanyBranchId",
                table: "CompanyContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyContacts",
                table: "CompanyContacts");

            migrationBuilder.RenameTable(
                name: "CompanyContacts",
                newName: "CompanyContact");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContact",
                newName: "IX_CompanyContact_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyContacts_CompanyBranchId",
                table: "CompanyContact",
                newName: "IX_CompanyContact_CompanyBranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyContact",
                table: "CompanyContact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_CompanyContact_CompanyContactId",
                table: "CompanyBranches",
                column: "CompanyContactId",
                principalTable: "CompanyContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_Companies_CompanyId",
                table: "CompanyContact",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_CompanyBranches_CompanyBranchId",
                table: "CompanyContact",
                column: "CompanyBranchId",
                principalTable: "CompanyBranches",
                principalColumn: "Id");
        }
    }
}
