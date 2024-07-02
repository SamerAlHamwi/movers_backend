using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RefactorServiceValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Services_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropIndex(
                name: "IX_ServiceValues_ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "IsForCompany",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "IsForUser",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "SubServiceId",
                table: "ServiceValues");

            migrationBuilder.AddColumn<byte>(
                name: "ServiceValueType",
                table: "ServiceValues",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ToolRelationType",
                table: "ServiceValues",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceValueType",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "ToolRelationType",
                table: "ServiceValues");

            migrationBuilder.AddColumn<bool>(
                name: "IsForCompany",
                table: "ServiceValues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForUser",
                table: "ServiceValues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ServiceValues",
                type: "float",
                nullable: true);

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
    }
}
