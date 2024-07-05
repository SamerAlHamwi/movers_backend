using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTranslationAndAddSubService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_RequestForQuotations_RequestForQuotationId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_RequestForQuotationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CoatingService",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "InstallationService",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsAnElevator",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NumberOfAdhesive",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NumberOfCardboardBoxes",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "NumberOfVehicleForHeavyFurniture",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PackagingMaterials",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PackagingService",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RequestForQuotationId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UnpackingService",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "RequestForQuotations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_ServiceTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTranslations_Services_CoreId",
                        column: x => x.CoreId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SubServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubServiceTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_SubServiceTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubServiceTranslations_SubServices_CoreId",
                        column: x => x.CoreId,
                        principalTable: "SubServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestForQuotations_ServiceId",
                table: "RequestForQuotations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTranslations_CoreId",
                table: "ServiceTranslations",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServices_ServiceId",
                table: "SubServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServiceTranslations_CoreId",
                table: "SubServiceTranslations",
                column: "CoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Services_ServiceId",
                table: "RequestForQuotations",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Services_ServiceId",
                table: "RequestForQuotations");

            migrationBuilder.DropTable(
                name: "ServiceTranslations");

            migrationBuilder.DropTable(
                name: "SubServiceTranslations");

            migrationBuilder.DropTable(
                name: "SubServices");

            migrationBuilder.DropIndex(
                name: "IX_RequestForQuotations_ServiceId",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "RequestForQuotations");

            migrationBuilder.AddColumn<bool>(
                name: "CoatingService",
                table: "Services",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InstallationService",
                table: "Services",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnElevator",
                table: "Services",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAdhesive",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfCardboardBoxes",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVehicleForHeavyFurniture",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PackagingMaterials",
                table: "Services",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PackagingService",
                table: "Services",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestForQuotationId",
                table: "Services",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "UnpackingService",
                table: "Services",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_RequestForQuotationId",
                table: "Services",
                column: "RequestForQuotationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_RequestForQuotations_RequestForQuotationId",
                table: "Services",
                column: "RequestForQuotationId",
                principalTable: "RequestForQuotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
