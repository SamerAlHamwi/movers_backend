using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class EditOnMediatorCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Mediator",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Mediator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Mediator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Mediator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mediator_CityId",
                table: "Mediator",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mediator_Cities_CityId",
                table: "Mediator",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mediator_Cities_CityId",
                table: "Mediator");

            migrationBuilder.DropIndex(
                name: "IX_Mediator_CityId",
                table: "Mediator");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Mediator");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Mediator");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Mediator");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Mediator");
        }
    }
}
