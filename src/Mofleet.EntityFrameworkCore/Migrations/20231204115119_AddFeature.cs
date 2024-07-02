using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForFeature",
                table: "PointsValue",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForFeature",
                table: "Points",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberInMonths",
                table: "Points",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndFeatureSubscribtionDate",
                table: "CompanyBranches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeature",
                table: "CompanyBranches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartFeatureSubscribtionDate",
                table: "CompanyBranches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndFeatureSubscribtionDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeature",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartFeatureSubscribtionDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForFeature",
                table: "PointsValue");

            migrationBuilder.DropColumn(
                name: "IsForFeature",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "NumberInMonths",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "EndFeatureSubscribtionDate",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "IsFeature",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "StartFeatureSubscribtionDate",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "EndFeatureSubscribtionDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsFeature",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "StartFeatureSubscribtionDate",
                table: "Companies");
        }
    }
}
