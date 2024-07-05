using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddAttributeChoiceAndAttachmentToRequestForQuotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RequestForQuotationId",
                table: "AttributeChoiceAndAttachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeChoiceAndAttachments_RequestForQuotationId",
                table: "AttributeChoiceAndAttachments",
                column: "RequestForQuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeChoiceAndAttachments_RequestForQuotations_RequestForQuotationId",
                table: "AttributeChoiceAndAttachments",
                column: "RequestForQuotationId",
                principalTable: "RequestForQuotations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeChoiceAndAttachments_RequestForQuotations_RequestForQuotationId",
                table: "AttributeChoiceAndAttachments");

            migrationBuilder.DropIndex(
                name: "IX_AttributeChoiceAndAttachments_RequestForQuotationId",
                table: "AttributeChoiceAndAttachments");

            migrationBuilder.DropColumn(
                name: "RequestForQuotationId",
                table: "AttributeChoiceAndAttachments");
        }
    }
}
