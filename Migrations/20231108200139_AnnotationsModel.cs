using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasusWebApps.Migrations
{
    /// <inheritdoc />
    public partial class AnnotationsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTags_ImageHandlers_ImageHandlerId",
                table: "ImageTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags");

            migrationBuilder.RenameTable(
                name: "ImageTags",
                newName: "ImageTag");

            migrationBuilder.RenameIndex(
                name: "IX_ImageTags_ImageHandlerId",
                table: "ImageTag",
                newName: "IX_ImageTag_ImageHandlerId");

            migrationBuilder.AlterColumn<int>(
                name: "ObjectType",
                table: "ImageTag",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTag",
                table: "ImageTag",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AnnotationModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoundingBoxX = table.Column<int>(type: "int", nullable: false),
                    BoundingBoxY = table.Column<int>(type: "int", nullable: false),
                    BoundingBoxWidth = table.Column<int>(type: "int", nullable: false),
                    BoundingBoxHeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnotationModels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTag_ImageHandlers_ImageHandlerId",
                table: "ImageTag",
                column: "ImageHandlerId",
                principalTable: "ImageHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTag_ImageHandlers_ImageHandlerId",
                table: "ImageTag");

            migrationBuilder.DropTable(
                name: "AnnotationModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTag",
                table: "ImageTag");

            migrationBuilder.RenameTable(
                name: "ImageTag",
                newName: "ImageTags");

            migrationBuilder.RenameIndex(
                name: "IX_ImageTag_ImageHandlerId",
                table: "ImageTags",
                newName: "IX_ImageTags_ImageHandlerId");

            migrationBuilder.AlterColumn<string>(
                name: "ObjectType",
                table: "ImageTags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTags_ImageHandlers_ImageHandlerId",
                table: "ImageTags",
                column: "ImageHandlerId",
                principalTable: "ImageHandlers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
