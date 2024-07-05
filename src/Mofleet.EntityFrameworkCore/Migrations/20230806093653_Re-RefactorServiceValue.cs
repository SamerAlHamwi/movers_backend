using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class ReRefactorServiceValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ServiceValues_ServiceValueId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_ServiceValueId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ServiceValueId",
                table: "Tools");

            migrationBuilder.AlterColumn<byte>(
                name: "ToolRelationType",
                table: "ServiceValues",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ServiceValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubServiceId",
                table: "ServiceValues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToolId",
                table: "ServiceValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "SubServiceId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "ToolId",
                table: "ServiceValues");

            migrationBuilder.AddColumn<long>(
                name: "ServiceValueId",
                table: "Tools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "ToolRelationType",
                table: "ServiceValues",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_ServiceValueId",
                table: "Tools",
                column: "ServiceValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ServiceValues_ServiceValueId",
                table: "Tools",
                column: "ServiceValueId",
                principalTable: "ServiceValues",
                principalColumn: "Id");
        }
    }
}
