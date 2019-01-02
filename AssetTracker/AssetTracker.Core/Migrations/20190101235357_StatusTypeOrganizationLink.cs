using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracker.Core.Migrations
{
    public partial class StatusTypeOrganizationLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Types",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Statuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Types_OrganizationId",
                table: "Types",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_OrganizationId",
                table: "Statuses",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Organizations_OrganizationId",
                table: "Statuses",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Organizations_OrganizationId",
                table: "Types",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Organizations_OrganizationId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Organizations_OrganizationId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Types_OrganizationId",
                table: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_OrganizationId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Statuses");
        }
    }
}
