using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMediatorClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediatorProfit",
                table: "Mediator");

            migrationBuilder.AlterColumn<double>(
                name: "CommissionPercentage",
                table: "Mediator",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionPercentage",
                table: "Mediator",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "MediatorProfit",
                table: "Mediator",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
