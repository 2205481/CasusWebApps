using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasusWebApps.Migrations
{
    /// <inheritdoc />
    public partial class NewNoType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "ImageHandlers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "ImageHandlers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
