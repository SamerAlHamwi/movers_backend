using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddPossibledDataTimr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinationPlaceNameByGoogle",
                table: "RequestForQuotations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PossibledDate",
                table: "RequestForQuotations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourcePlaceNameByGoogle",
                table: "RequestForQuotations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationPlaceNameByGoogle",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "PossibledDate",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "SourcePlaceNameByGoogle",
                table: "RequestForQuotations");
        }
    }
}
