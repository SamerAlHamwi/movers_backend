using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeChoiceIntoAttributeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "AttributeForSourceTypeValues");

            migrationBuilder.AddColumn<int>(
                name: "AttributeChoiceId",
                table: "AttributeForSourceTypeValues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeForSourceTypeValues_AttributeChoiceId",
                table: "AttributeForSourceTypeValues",
                column: "AttributeChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeForSourceTypeValues_AttributeChoices_AttributeChoiceId",
                table: "AttributeForSourceTypeValues",
                column: "AttributeChoiceId",
                principalTable: "AttributeChoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeForSourceTypeValues_AttributeChoices_AttributeChoiceId",
                table: "AttributeForSourceTypeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeForSourceTypeValues_AttributeChoiceId",
                table: "AttributeForSourceTypeValues");

            migrationBuilder.DropColumn(
                name: "AttributeChoiceId",
                table: "AttributeForSourceTypeValues");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "AttributeForSourceTypeValues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
