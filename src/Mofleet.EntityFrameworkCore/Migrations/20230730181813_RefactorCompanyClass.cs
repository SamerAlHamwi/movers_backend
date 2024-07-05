using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCompanyClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AbpUsers_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Companies_CompanyId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CompanyId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ServiceValues",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Companies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CompanyBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyContactId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    ServiceValueId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_CompanyBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBranch_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyBranch_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyBranch_ServiceValues_ServiceValueId",
                        column: x => x.ServiceValueId,
                        principalTable: "ServiceValues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DialCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyBranchId = table.Column<int>(type: "int", nullable: true),
                    IsForBranchCompany = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_CompanyContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContact_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyContact_CompanyBranch_CompanyBranchId",
                        column: x => x.CompanyBranchId,
                        principalTable: "CompanyBranch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValues_CompanyId",
                table: "ServiceValues",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranch_CityId",
                table: "CompanyBranch",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranch_CompanyContactId",
                table: "CompanyBranch",
                column: "CompanyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranch_CompanyId",
                table: "CompanyBranch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranch_ServiceValueId",
                table: "CompanyBranch",
                column: "ServiceValueId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_CompanyBranchId",
                table: "CompanyContact",
                column: "CompanyBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_CompanyId",
                table: "CompanyContact",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AbpUsers_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceValues_Companies_CompanyId",
                table: "ServiceValues",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyBranch_CompanyContact_CompanyContactId",
                table: "CompanyBranch",
                column: "CompanyContactId",
                principalTable: "CompanyContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AbpUsers_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceValues_Companies_CompanyId",
                table: "ServiceValues");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyBranch_CompanyContact_CompanyContactId",
                table: "CompanyBranch");

            migrationBuilder.DropTable(
                name: "CompanyContact");

            migrationBuilder.DropTable(
                name: "CompanyBranch");

            migrationBuilder.DropIndex(
                name: "IX_ServiceValues_CompanyId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ServiceValues");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Companies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_CompanyId",
                table: "Services",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AbpUsers_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Companies_CompanyId",
                table: "Services",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
