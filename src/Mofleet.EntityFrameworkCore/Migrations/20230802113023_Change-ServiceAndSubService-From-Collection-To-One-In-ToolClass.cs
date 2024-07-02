using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class ChangeServiceAndSubServiceFromCollectionToOneInToolClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceTool");

            migrationBuilder.DropTable(
                name: "SubServiceTool");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Tools",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubServiceId",
                table: "Tools",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ServiceId",
                table: "Tools",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_SubServiceId",
                table: "Tools",
                column: "SubServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Services_ServiceId",
                table: "Tools",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_SubServices_SubServiceId",
                table: "Tools",
                column: "SubServiceId",
                principalTable: "SubServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Services_ServiceId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_SubServices_SubServiceId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_ServiceId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_SubServiceId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "SubServiceId",
                table: "Tools");

            migrationBuilder.CreateTable(
                name: "ServiceTool",
                columns: table => new
                {
                    ServicesId = table.Column<int>(type: "int", nullable: false),
                    ToolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTool", x => new { x.ServicesId, x.ToolsId });
                    table.ForeignKey(
                        name: "FK_ServiceTool_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTool_Tools_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubServiceTool",
                columns: table => new
                {
                    SubServicesId = table.Column<int>(type: "int", nullable: false),
                    ToolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubServiceTool", x => new { x.SubServicesId, x.ToolsId });
                    table.ForeignKey(
                        name: "FK_SubServiceTool_SubServices_SubServicesId",
                        column: x => x.SubServicesId,
                        principalTable: "SubServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubServiceTool_Tools_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTool_ToolsId",
                table: "ServiceTool",
                column: "ToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubServiceTool_ToolsId",
                table: "SubServiceTool",
                column: "ToolsId");
        }
    }
}
