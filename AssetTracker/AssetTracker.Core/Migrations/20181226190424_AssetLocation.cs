using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracker.Core.Migrations
{
    public partial class AssetLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CreateDt",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Locations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "AssetLocation",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    CreateDt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLocation", x => new { x.AssetId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_AssetLocation_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetLocation_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetLocation_LocationId",
                table: "AssetLocation",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "AssetLocation");

            migrationBuilder.AlterColumn<int>(
                name: "AssetId",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDt",
                table: "Locations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Assets_AssetId",
                table: "Locations",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
