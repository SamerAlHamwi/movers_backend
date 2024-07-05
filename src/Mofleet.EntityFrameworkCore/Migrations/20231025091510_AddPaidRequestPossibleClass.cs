using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddPaidRequestPossibleClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaidRequestPossibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: true),
                    NumberOfPaidPoints = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PaidRequestPossibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaidRequestPossibles_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaidRequestPossibles_CompanyBranches_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaidRequestPossibles_RequestForQuotations_RequestId",
                        column: x => x.RequestId,
                        principalTable: "RequestForQuotations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaidRequestPossibles_CompanyBranchId",
                table: "PaidRequestPossibles",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidRequestPossibles_CompanyId",
                table: "PaidRequestPossibles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaidRequestPossibles_RequestId",
                table: "PaidRequestPossibles",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaidRequestPossibles");
        }
    }
}
