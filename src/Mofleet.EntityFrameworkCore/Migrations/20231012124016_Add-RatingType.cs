using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RatingType",
                table: "Ratings",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingType",
                table: "Ratings");
        }
    }
}
