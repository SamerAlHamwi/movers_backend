using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RemoveToolsFromService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Services_ServiceId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_ServiceId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ToolRelationType",
                table: "ServiceValues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Tools",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ToolRelationType",
                table: "ServiceValues",
                type: "tinyint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ServiceId",
                table: "Tools",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Services_ServiceId",
                table: "Tools",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
