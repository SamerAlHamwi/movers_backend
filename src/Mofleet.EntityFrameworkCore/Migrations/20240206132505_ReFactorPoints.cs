using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class ReFactorPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainForPoints",
                table: "SourceTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PointsToBuyRequest",
                table: "AttributeChoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointsToGiftToCompany",
                table: "AttributeChoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainForPoints",
                table: "SourceTypes");

            migrationBuilder.DropColumn(
                name: "PointsToBuyRequest",
                table: "AttributeChoices");

            migrationBuilder.DropColumn(
                name: "PointsToGiftToCompany",
                table: "AttributeChoices");
        }
    }
}
