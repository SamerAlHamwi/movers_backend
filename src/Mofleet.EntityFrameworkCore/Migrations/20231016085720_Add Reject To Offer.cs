using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddRejectToOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectReasonDescription",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RejectReasonId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RejectReasonId",
                table: "Offers",
                column: "RejectReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_RejectReasons_RejectReasonId",
                table: "Offers",
                column: "RejectReasonId",
                principalTable: "RejectReasons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_RejectReasons_RejectReasonId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_RejectReasonId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RejectReasonDescription",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RejectReasonId",
                table: "Offers");
        }
    }
}
