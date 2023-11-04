using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasusWebApps.Migrations
{
    /// <inheritdoc />
    public partial class ImageTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeWindow = table.Column<float>(type: "real", nullable: false),
                    ImageHandlerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageTags_ImageHandlers_ImageHandlerId",
                        column: x => x.ImageHandlerId,
                        principalTable: "ImageHandlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_ImageHandlerId",
                table: "ImageTags",
                column: "ImageHandlerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageTags");
        }
    }
}
