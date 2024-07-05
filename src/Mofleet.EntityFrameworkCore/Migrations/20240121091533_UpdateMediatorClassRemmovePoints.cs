using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMediatorClassRemmovePoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsToGiftMediator",
                table: "SourceTypes");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Mediator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsToGiftMediator",
                table: "SourceTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Mediator",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
