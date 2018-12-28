using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracker.Core.Migrations
{
    public partial class Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_AssetId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assets");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Assets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDt",
                table: "AssetLocation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_StatusId",
                table: "Assets",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Statuses_StatusId",
                table: "Assets",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Statuses_StatusId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Assets_StatusId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "TransferDt",
                table: "AssetLocation");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Assets",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AssetId",
                table: "Locations",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
