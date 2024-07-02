using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddAcceptRequestsAndAcceptPossibleRequestsForCompanyAndBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptPossibleRequests",
                table: "CompanyBranches",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptRequests",
                table: "CompanyBranches",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptPossibleRequests",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptRequests",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptPossibleRequests",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "AcceptRequests",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "AcceptPossibleRequests",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AcceptRequests",
                table: "Companies");
        }
    }
}
