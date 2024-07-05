using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddFinishForRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmFinishDateByCompany",
                table: "RequestForQuotations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmFinishDateByUser",
                table: "RequestForQuotations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedDate",
                table: "RequestForQuotations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonOfNotFinish",
                table: "RequestForQuotations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmFinishDateByCompany",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "ConfirmFinishDateByUser",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "FinishedDate",
                table: "RequestForQuotations");

            migrationBuilder.DropColumn(
                name: "ReasonOfNotFinish",
                table: "RequestForQuotations");
        }
    }
}
