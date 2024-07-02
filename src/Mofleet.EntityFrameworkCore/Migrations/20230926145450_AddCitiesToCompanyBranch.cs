using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddCitiesToCompanyBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityCompanyBranch",
                columns: table => new
                {
                    AvailableCitiesId = table.Column<int>(type: "int", nullable: false),
                    CompanyBranchesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityCompanyBranch", x => new { x.AvailableCitiesId, x.CompanyBranchesId });
                    table.ForeignKey(
                        name: "FK_CityCompanyBranch_Cities_AvailableCitiesId",
                        column: x => x.AvailableCitiesId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityCompanyBranch_CompanyBranches_CompanyBranchesId",
                        column: x => x.CompanyBranchesId,
                        principalTable: "CompanyBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityCompanyBranch_CompanyBranchesId",
                table: "CityCompanyBranch",
                column: "CompanyBranchesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityCompanyBranch");
        }
    }
}
