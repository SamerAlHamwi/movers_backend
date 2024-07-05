using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class ConvertOneObjectToListInAttachmentAndAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeChoiceAndAttachments_Attachments_AttachmentId",
                table: "AttributeChoiceAndAttachments");

            migrationBuilder.DropIndex(
                name: "IX_AttributeChoiceAndAttachments_AttachmentId",
                table: "AttributeChoiceAndAttachments");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "AttributeChoiceAndAttachments");

            migrationBuilder.AddColumn<int>(
                name: "AttributeChoiceAndAttachmentId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttributeChoiceAndAttachmentId",
                table: "Attachments",
                column: "AttributeChoiceAndAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AttributeChoiceAndAttachments_AttributeChoiceAndAttachmentId",
                table: "Attachments",
                column: "AttributeChoiceAndAttachmentId",
                principalTable: "AttributeChoiceAndAttachments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AttributeChoiceAndAttachments_AttributeChoiceAndAttachmentId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_AttributeChoiceAndAttachmentId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "AttributeChoiceAndAttachmentId",
                table: "Attachments");

            migrationBuilder.AddColumn<long>(
                name: "AttachmentId",
                table: "AttributeChoiceAndAttachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeChoiceAndAttachments_AttachmentId",
                table: "AttributeChoiceAndAttachments",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeChoiceAndAttachments_Attachments_AttachmentId",
                table: "AttributeChoiceAndAttachments",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
