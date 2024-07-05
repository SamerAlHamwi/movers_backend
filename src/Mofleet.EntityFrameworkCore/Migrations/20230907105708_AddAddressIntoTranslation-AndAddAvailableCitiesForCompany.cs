using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressIntoTranslationAndAddAvailableCitiesForCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CompanyTranslations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Companies_CompanyId",
                table: "Cities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Companies_CompanyId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "CompanyTranslations");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
