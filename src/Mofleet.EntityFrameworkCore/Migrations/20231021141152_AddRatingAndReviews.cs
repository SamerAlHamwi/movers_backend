using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingAndReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: true),
                    RatingType = table.Column<byte>(type: "tinyint", nullable: false),
                    Quality = table.Column<double>(type: "float", nullable: false),
                    CustomerService = table.Column<double>(type: "float", nullable: false),
                    ValueOfServiceForMoney = table.Column<double>(type: "float", nullable: false),
                    OverallRating = table.Column<double>(type: "float", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_CompanyBranches_CompanyBranchId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyBranchId",
                table: "Reviews",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Ratings_RatingId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranches_Ratings_RatingId",
                table: "CompanyBranches");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reviews");

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
    }
}
