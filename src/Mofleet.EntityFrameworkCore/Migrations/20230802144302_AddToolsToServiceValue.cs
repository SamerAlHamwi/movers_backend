using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddToolsToServiceValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues");

            migrationBuilder.AddColumn<long>(
                name: "ServiceValueId",
                table: "Tools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceValues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ServiceValueId",
                table: "Tools",
                column: "ServiceValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ServiceValues_ServiceValueId",
                table: "Tools",
                column: "ServiceValueId",
                principalTable: "ServiceValues",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ServiceValues_ServiceValueId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_ServiceValueId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ServiceValueId",
                table: "Tools");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceValues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
