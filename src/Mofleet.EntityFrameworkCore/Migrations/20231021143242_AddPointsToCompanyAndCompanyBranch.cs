using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsToCompanyAndCompanyBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfGiftedPoints",
                table: "CompanyBranches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPaidPoints",
                table: "CompanyBranches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGiftedPoints",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPaidPoints",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfGiftedPoints",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "NumberOfPaidPoints",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "NumberOfGiftedPoints",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "NumberOfPaidPoints",
                table: "Companies");
        }
    }
}
