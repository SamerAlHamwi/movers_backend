using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class EditNameColumnInRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quality",
                table: "Ratings",
                newName: "Quality");

            migrationBuilder.RenameColumn(
                name: "AverageRate",
                table: "Ratings",
                newName: "OverallRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "Ratings",
                newName: "quality");

            migrationBuilder.RenameColumn(
                name: "OverallRating",
                table: "Ratings",
                newName: "AverageRate");
        }
    }
}
