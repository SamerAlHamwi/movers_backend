using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTypeEnumToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_Cities_CityId",
                table: "CompanyBranch");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "CompanyBranch",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ServiceType",
                table: "Companies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_Cities_CityId",
                table: "CompanyBranch",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_Cities_CityId",
                table: "CompanyBranch");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "CompanyBranch",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_Cities_CityId",
                table: "CompanyBranch",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
