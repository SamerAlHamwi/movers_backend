using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTypeForCompanyAndBranchesAndRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ServiceType",
                table: "RequestForQuotations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ServiceType",
                table: "CompanyBranches",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ServiceType",
                table: "Companies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Companies");
        }
    }
}
