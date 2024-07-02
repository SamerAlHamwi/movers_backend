using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Mofleet.Migrations
{
    /// <inheritdoc />
    public partial class AddDrafts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttributeAndAttachmentsForDraftId",
                table: "Attachments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceTypeId = table.Column<int>(type: "int", nullable: true),
                    SourceLongitude = table.Column<double>(type: "float", nullable: true),
                    SourceLatitude = table.Column<double>(type: "float", nullable: true),
                    SourceCityId = table.Column<int>(type: "int", nullable: true),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoveAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DestinationLongitude = table.Column<double>(type: "float", nullable: true),
                    DestinationLatitude = table.Column<double>(type: "float", nullable: true),
                    DestinationCityId = table.Column<int>(type: "int", nullable: true),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceType = table.Column<byte>(type: "tinyint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drafts_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeAndAttachmentsForDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DraftId = table.Column<int>(type: "int", nullable: false),
                    AttributeChoiceId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_AttributeAndAttachmentsForDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeAndAttachmentsForDrafts_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeForSourceTypeValuesForDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DraftId = table.Column<int>(type: "int", nullable: false),
                    AttributeForSourcTypeId = table.Column<int>(type: "int", nullable: false),
                    AttributeChoiceId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AttributeForSourceTypeValuesForDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeForSourceTypeValuesForDrafts_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestForQuotationContactsForDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWhatsAppAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsTelegramAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsCallAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DraftId = table.Column<int>(type: "int", nullable: false),
                    RequestForQuotationContactType = table.Column<byte>(type: "tinyint", nullable: false),
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
                    table.PrimaryKey("PK_RequestForQuotationContactsForDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestForQuotationContactsForDrafts_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceValuesForDrafts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DraftId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    SubServiceId = table.Column<int>(type: "int", nullable: true),
                    ToolId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_ServiceValuesForDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceValuesForDrafts_Drafts_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Drafts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttributeAndAttachmentsForDraftId",
                table: "Attachments",
                column: "AttributeAndAttachmentsForDraftId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeAndAttachmentsForDrafts_DraftId",
                table: "AttributeAndAttachmentsForDrafts",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeForSourceTypeValuesForDrafts_DraftId",
                table: "AttributeForSourceTypeValuesForDrafts",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_UserId",
                table: "Drafts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForQuotationContactsForDrafts_DraftId",
                table: "RequestForQuotationContactsForDrafts",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceValuesForDrafts_DraftId",
                table: "ServiceValuesForDrafts",
                column: "DraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AttributeAndAttachmentsForDrafts_AttributeAndAttachmentsForDraftId",
                table: "Attachments",
                column: "AttributeAndAttachmentsForDraftId",
                principalTable: "AttributeAndAttachmentsForDrafts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AttributeAndAttachmentsForDrafts_AttributeAndAttachmentsForDraftId",
                table: "Attachments");

            migrationBuilder.DropTable(
                name: "AttributeAndAttachmentsForDrafts");

            migrationBuilder.DropTable(
                name: "AttributeForSourceTypeValuesForDrafts");

            migrationBuilder.DropTable(
                name: "RequestForQuotationContactsForDrafts");

            migrationBuilder.DropTable(
                name: "ServiceValuesForDrafts");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_AttributeAndAttachmentsForDraftId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "AttributeAndAttachmentsForDraftId",
                table: "Attachments");
        }
    }
}
