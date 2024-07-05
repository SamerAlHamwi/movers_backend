using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddSourceTypeAsCollectionInAttributeForSourceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeForSourcTypes_SourceTypes_SourceTypeId",
                table: "AttributeForSourcTypes");

            migrationBuilder.DropIndex(
                name: "IX_AttributeForSourcTypes_SourceTypeId",
                table: "AttributeForSourcTypes");

            migrationBuilder.DropColumn(
                name: "SourceTypeId",
                table: "AttributeForSourcTypes");

            migrationBuilder.CreateTable(
                name: "AttributeForSourceTypeSourceType",
                columns: table => new
                {
                    AttributesId = table.Column<int>(type: "int", nullable: false),
                    SourceTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeForSourceTypeSourceType", x => new { x.AttributesId, x.SourceTypesId });
                    table.ForeignKey(
                        name: "FK_AttributeForSourceTypeSourceType_AttributeForSourcTypes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "AttributeForSourcTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeForSourceTypeSourceType_SourceTypes_SourceTypesId",
                        column: x => x.SourceTypesId,
                        principalTable: "SourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeForSourceTypeSourceType_SourceTypesId",
                table: "AttributeForSourceTypeSourceType",
                column: "SourceTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeForSourceTypeSourceType");

            migrationBuilder.AddColumn<int>(
                name: "SourceTypeId",
                table: "AttributeForSourcTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeForSourcTypes_SourceTypeId",
                table: "AttributeForSourcTypes",
                column: "SourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeForSourcTypes_SourceTypes_SourceTypeId",
                table: "AttributeForSourcTypes",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id");
        }
    }
}
