using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRegionFromTheSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Regions_DestinationRegionId",
                table: "RequestForQuotations");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Regions_SourceRegionId",
                table: "RequestForQuotations");

            migrationBuilder.DropTable(
                name: "RegionTranslations");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.RenameColumn(
                name: "SourceRegionId",
                table: "RequestForQuotations",
                newName: "SourceCityId");

            migrationBuilder.RenameColumn(
                name: "DestinationRegionId",
                table: "RequestForQuotations",
                newName: "DestinationCityId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestForQuotations_SourceRegionId",
                table: "RequestForQuotations",
                newName: "IX_RequestForQuotations_SourceCityId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestForQuotations_DestinationRegionId",
                table: "RequestForQuotations",
                newName: "IX_RequestForQuotations_DestinationCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Cities_DestinationCityId",
                table: "RequestForQuotations",
                column: "DestinationCityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Cities_SourceCityId",
                table: "RequestForQuotations",
                column: "SourceCityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Cities_DestinationCityId",
                table: "RequestForQuotations");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Cities_SourceCityId",
                table: "RequestForQuotations");

            migrationBuilder.RenameColumn(
                name: "SourceCityId",
                table: "RequestForQuotations",
                newName: "SourceRegionId");

            migrationBuilder.RenameColumn(
                name: "DestinationCityId",
                table: "RequestForQuotations",
                newName: "DestinationRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestForQuotations_SourceCityId",
                table: "RequestForQuotations",
                newName: "IX_RequestForQuotations_SourceRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestForQuotations_DestinationCityId",
                table: "RequestForQuotations",
                newName: "IX_RequestForQuotations_DestinationRegionId");

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegionTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionTranslations_Regions_CoreId",
                        column: x => x.CoreId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CityId",
                table: "Regions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionTranslations_CoreId",
                table: "RegionTranslations",
                column: "CoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Regions_DestinationRegionId",
                table: "RequestForQuotations",
                column: "DestinationRegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Regions_SourceRegionId",
                table: "RequestForQuotations",
                column: "SourceRegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
