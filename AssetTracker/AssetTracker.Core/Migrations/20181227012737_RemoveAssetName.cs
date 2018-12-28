using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracker.Core.Migrations
{
    public partial class RemoveAssetName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Assets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Assets",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
