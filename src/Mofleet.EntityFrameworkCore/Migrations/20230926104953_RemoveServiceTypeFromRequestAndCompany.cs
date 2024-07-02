using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RemoveServiceTypeFromRequestAndCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "FirstNameContact",
                table: "RequestForQuotationContacts");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "LastNameContact",
                table: "RequestForQuotationContacts",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "RequestForQuotationContacts",
                newName: "LastNameContact");

            migrationBuilder.AddColumn<byte>(
                name: "ServiceType",
                table: "RequestForQuotations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameContact",
                table: "RequestForQuotationContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ServiceType",
                table: "Companies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
