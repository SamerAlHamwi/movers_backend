using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_Cities_CityId",
                table: "CompanyBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_Companies_CompanyId",
                table: "CompanyBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_CompanyContact_CompanyContactId",
                table: "CompanyBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_ServiceValues_ServiceValueId",
                table: "CompanyBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_CompanyBranch_CompanyBranchId",
                table: "CompanyContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyBranch",
                table: "CompanyBranch");

            migrationBuilder.RenameTable(
                name: "CompanyBranch",
                newName: "CompanyBranches");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranch_ServiceValueId",
                table: "CompanyBranches",
                newName: "IX_CompanyBranches_ServiceValueId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranch_CompanyId",
                table: "CompanyBranches",
                newName: "IX_CompanyBranches_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranch_CompanyContactId",
                table: "CompanyBranches",
                newName: "IX_CompanyBranches_CompanyContactId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranch_CityId",
                table: "CompanyBranches",
                newName: "IX_CompanyBranches_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyBranches",
                table: "CompanyBranches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_Cities_CityId",
                table: "CompanyBranches",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_Companies_CompanyId",
                table: "CompanyBranches",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_CompanyContact_CompanyContactId",
                table: "CompanyBranches",
                column: "CompanyContactId",
                principalTable: "CompanyContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_ServiceValues_ServiceValueId",
                table: "CompanyBranches",
                column: "ServiceValueId",
                principalTable: "ServiceValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_CompanyBranches_CompanyBranchId",
                table: "CompanyContact",
                column: "CompanyBranchId",
                principalTable: "CompanyBranches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_Cities_CityId",
                table: "CompanyBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_Companies_CompanyId",
                table: "CompanyBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_CompanyContact_CompanyContactId",
                table: "CompanyBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_ServiceValues_ServiceValueId",
                table: "CompanyBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_CompanyBranches_CompanyBranchId",
                table: "CompanyContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyBranches",
                table: "CompanyBranches");

            migrationBuilder.RenameTable(
                name: "CompanyBranches",
                newName: "CompanyBranch");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranches_ServiceValueId",
                table: "CompanyBranch",
                newName: "IX_CompanyBranch_ServiceValueId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranches_CompanyId",
                table: "CompanyBranch",
                newName: "IX_CompanyBranch_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranches_CompanyContactId",
                table: "CompanyBranch",
                newName: "IX_CompanyBranch_CompanyContactId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyBranches_CityId",
                table: "CompanyBranch",
                newName: "IX_CompanyBranch_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyBranch",
                table: "CompanyBranch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_Cities_CityId",
                table: "CompanyBranch",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_Companies_CompanyId",
                table: "CompanyBranch",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_CompanyContact_CompanyContactId",
                table: "CompanyBranch",
                column: "CompanyContactId",
                principalTable: "CompanyContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_ServiceValues_ServiceValueId",
                table: "CompanyBranch",
                column: "ServiceValueId",
                principalTable: "ServiceValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_CompanyBranch_CompanyBranchId",
                table: "CompanyContact",
                column: "CompanyBranchId",
                principalTable: "CompanyBranch",
                principalColumn: "Id");
        }
    }
}
