using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Services_ServiceId",
                table: "RequestForQuotations");

            migrationBuilder.DropIndex(
                name: "IX_RequestForQuotations_ServiceId",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "RequestForQuotations");

            migrationBuilder.CreateTable(
                name: "ServiceValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    SubServiceId = table.Column<int>(type: "int", nullable: true),
                    RequestForQuotationId = table.Column<long>(type: "bigint", nullable: true),
                    IsForUser = table.Column<bool>(type: "bit", nullable: false),
                    IsForCompany = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
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
                    table.PrimaryKey("PK_ServiceValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceValues_RequestForQuotations_RequestForQuotationId",
                        column: x => x.RequestForQuotationId,
                        principalTable: "RequestForQuotations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceValues_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_RequestForQuotationId",
                table: "ServiceValues",
                column: "RequestForQuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceValues");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "RequestForQuotations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestForQuotations_ServiceId",
                table: "RequestForQuotations",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Services_ServiceId",
                table: "RequestForQuotations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
