using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RefactorSelectedCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_SelectedCompaniesByUsers_SelectedCompaniesByUserId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "SelectedCompaniesByUsers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SelectedCompaniesByUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SelectedCompaniesByUserId",
                table: "Companies");

            migrationBuilder.CreateTable(
                name: "SelectedCompaniesBySystemForRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestForQuotationId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedCompaniesBySystemForRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedCompaniesBySystemForRequests_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedCompaniesBySystemForRequests_CompanyBranches_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedCompaniesBySystemForRequests_RequestForQuotations_RequestForQuotationId",
                        column: x => x.RequestForQuotationId,
                        principalTable: "RequestForQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedCompaniesBySystemForRequests_CompanyBranchId",
                table: "SelectedCompaniesBySystemForRequests",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedCompaniesBySystemForRequests_CompanyId",
                table: "SelectedCompaniesBySystemForRequests",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedCompaniesBySystemForRequests_RequestForQuotationId",
                table: "SelectedCompaniesBySystemForRequests",
                column: "RequestForQuotationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedCompaniesBySystemForRequests");

            migrationBuilder.AddColumn<int>(
                name: "SelectedCompaniesByUserId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SelectedCompaniesByUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestForQuotationId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedCompaniesByUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedCompaniesByUsers_RequestForQuotations_RequestForQuotationId",
                        column: x => x.RequestForQuotationId,
                        principalTable: "RequestForQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SelectedCompaniesByUserId",
                table: "Companies",
                column: "SelectedCompaniesByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedCompaniesByUsers_RequestForQuotationId",
                table: "SelectedCompaniesByUsers",
                column: "RequestForQuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_SelectedCompaniesByUsers_SelectedCompaniesByUserId",
                table: "Companies",
                column: "SelectedCompaniesByUserId",
                principalTable: "SelectedCompaniesByUsers",
                principalColumn: "Id");
        }
    }
}
