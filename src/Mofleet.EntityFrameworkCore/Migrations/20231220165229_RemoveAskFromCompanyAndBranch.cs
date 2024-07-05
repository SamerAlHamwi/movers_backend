using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAskFromCompanyAndBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AskEditStatus",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "AskEditStatus",
                table: "Companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "AskEditStatus",
                table: "CompanyBranches",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "AskEditStatus",
                table: "Companies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
