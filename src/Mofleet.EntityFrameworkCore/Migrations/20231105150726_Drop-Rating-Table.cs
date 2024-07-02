using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class DropRatingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Ratings_RatingId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_Ratings_RatingId",
                table: "CompanyBranches");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_CompanyBranches_RatingId",
                table: "CompanyBranches");

            migrationBuilder.DropIndex(
                name: "IX_Companies_RatingId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "CompanyBranches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerService = table.Column<double>(type: "float", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    OverallRating = table.Column<double>(type: "float", nullable: false),
                    Quality = table.Column<double>(type: "float", nullable: false),
                    RatingType = table.Column<byte>(type: "tinyint", nullable: false),
                    ValueOfServiceForMoney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_CompanyBranches_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranches_RatingId",
                table: "CompanyBranches",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_RatingId",
                table: "Companies",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CompanyBranchId",
                table: "Ratings",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CompanyId",
                table: "Ratings",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Ratings_RatingId",
                table: "Companies",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranches_Ratings_RatingId",
                table: "CompanyBranches",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");
        }
    }
}
