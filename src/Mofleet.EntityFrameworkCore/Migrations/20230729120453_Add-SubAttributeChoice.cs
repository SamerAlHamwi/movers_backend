using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddSubAttributeChoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAttributeChoiceParent",
                table: "AttributeChoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "AttributeChoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeChoices_ParentId",
                table: "AttributeChoices",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeChoices_AttributeChoices_ParentId",
                table: "AttributeChoices",
                column: "ParentId",
                principalTable: "AttributeChoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeChoices_AttributeChoices_ParentId",
                table: "AttributeChoices");

            migrationBuilder.DropIndex(
                name: "IX_AttributeChoices_ParentId",
                table: "AttributeChoices");

            migrationBuilder.DropColumn(
                name: "IsAttributeChoiceParent",
                table: "AttributeChoices");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AttributeChoices");
        }
    }
}
