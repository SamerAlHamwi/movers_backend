using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class ReviweAndRatingInOneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CompanyBranches_CompanyBranchId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CompanyBranchId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CompanyBranchId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewProvideType",
                table: "Reviews");

            migrationBuilder.AddColumn<double>(
                name: "CustomerService",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OverallRating",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Quality",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValueOfServiceForMoney",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Offers_OfferId",
                table: "Reviews",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Offers_OfferId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OfferId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CustomerService",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OverallRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ValueOfServiceForMoney",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "CompanyBranchId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ReviewProvideType",
                table: "Reviews",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyBranchId",
                table: "Reviews",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CompanyId",
                table: "Reviews",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Companies_CompanyId",
                table: "Reviews",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CompanyBranches_CompanyBranchId",
                table: "Reviews",
                column: "CompanyBranchId",
                principalTable: "CompanyBranches",
                principalColumn: "Id");
        }
    }
}
