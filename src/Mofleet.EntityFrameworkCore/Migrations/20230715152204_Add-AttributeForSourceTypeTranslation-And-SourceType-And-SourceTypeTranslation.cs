using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeForSourceTypeTranslationAndSourceTypeAndSourceTypeTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppartmentType",
                table: "AttributeForSourcTypes");

            migrationBuilder.DropColumn(
                name: "PropertyForSourceLocationType",
                table: "AttributeForSourcTypes");

            migrationBuilder.DropColumn(
                name: "SourceLocationType",
                table: "AttributeForSourcTypes");

            migrationBuilder.AddColumn<int>(
                name: "SourceTypeId",
                table: "AttributeForSourcTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttributeForSourceTypeTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeForSourceTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeForSourceTypeTranslations_AttributeForSourcTypes_CoreId",
                        column: x => x.CoreId,
                        principalTable: "AttributeForSourcTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceTypeTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoreId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypeTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceTypeTranslations_SourceTypes_CoreId",
                        column: x => x.CoreId,
                        principalTable: "SourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeForSourcTypes_SourceTypeId",
                table: "AttributeForSourcTypes",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeForSourceTypeTranslations_CoreId",
                table: "AttributeForSourceTypeTranslations",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceTypeTranslations_CoreId",
                table: "SourceTypeTranslations",
                column: "CoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeForSourcTypes_SourceTypes_SourceTypeId",
                table: "AttributeForSourcTypes",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeForSourcTypes_SourceTypes_SourceTypeId",
                table: "AttributeForSourcTypes");

            migrationBuilder.DropTable(
                name: "AttributeForSourceTypeTranslations");

            migrationBuilder.DropTable(
                name: "SourceTypeTranslations");

            migrationBuilder.DropTable(
                name: "SourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_AttributeForSourcTypes_SourceTypeId",
                table: "AttributeForSourcTypes");

            migrationBuilder.DropColumn(
                name: "SourceTypeId",
                table: "AttributeForSourcTypes");

            migrationBuilder.AddColumn<byte>(
                name: "AppartmentType",
                table: "AttributeForSourcTypes",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "PropertyForSourceLocationType",
                table: "AttributeForSourcTypes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "SourceLocationType",
                table: "AttributeForSourcTypes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
