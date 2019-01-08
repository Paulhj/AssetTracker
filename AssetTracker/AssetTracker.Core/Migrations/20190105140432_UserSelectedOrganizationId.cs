using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracker.Core.Migrations
{
    public partial class UserSelectedOrganizationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedOrganizationId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedOrganizationId",
                table: "Users");
        }
    }
}
