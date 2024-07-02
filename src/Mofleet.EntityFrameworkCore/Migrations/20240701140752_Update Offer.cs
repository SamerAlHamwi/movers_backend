using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TruckId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_DriverId",
                table: "Offers",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_TruckId",
                table: "Offers",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Drivers_DriverId",
                table: "Offers",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Trucks_TruckId",
                table: "Offers",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Drivers_DriverId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Trucks_TruckId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_DriverId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_TruckId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "TruckId",
                table: "Offers");
        }
    }
}
