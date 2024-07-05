using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RenameParentToAttributeChoiceParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeChoices_AttributeChoices_ParentId",
                table: "AttributeChoices");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "AttributeChoices",
                newName: "AttributeChociceParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeChoices_ParentId",
                table: "AttributeChoices",
                newName: "IX_AttributeChoices_AttributeChociceParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeChoices_AttributeChoices_AttributeChociceParentId",
                table: "AttributeChoices",
                column: "AttributeChociceParentId",
                principalTable: "AttributeChoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeChoices_AttributeChoices_AttributeChociceParentId",
                table: "AttributeChoices");

            migrationBuilder.RenameColumn(
                name: "AttributeChociceParentId",
                table: "AttributeChoices",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeChoices_AttributeChociceParentId",
                table: "AttributeChoices",
                newName: "IX_AttributeChoices_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeChoices_AttributeChoices_ParentId",
                table: "AttributeChoices",
                column: "ParentId",
                principalTable: "AttributeChoices",
                principalColumn: "Id");
        }
    }
}
