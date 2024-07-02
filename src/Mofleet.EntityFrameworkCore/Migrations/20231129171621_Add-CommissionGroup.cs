using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddCommissionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommissionGroupId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommissionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_CommissionGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CommissionGroupId",
                table: "Companies",
                column: "CommissionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CommissionGroups_CommissionGroupId",
                table: "Companies",
                column: "CommissionGroupId",
                principalTable: "CommissionGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CommissionGroups_CommissionGroupId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CommissionGroups");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CommissionGroupId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CommissionGroupId",
                table: "Companies");
        }
    }
}
