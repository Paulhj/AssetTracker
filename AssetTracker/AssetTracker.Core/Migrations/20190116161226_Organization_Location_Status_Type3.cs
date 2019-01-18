using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracker.Core.Migrations
{
    public partial class Organization_Location_Status_Type3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLocation_Locations_LocationId",
                table: "AssetLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Statuses_StatusId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Types_TypeId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Organizations_OrganizationId",
                table: "Statuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Organizations_OrganizationId",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetLocation",
                table: "AssetLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Types_OrganizationId",
                table: "Type",
                newName: "IX_Type_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Statuses_OrganizationId",
                table: "Status",
                newName: "IX_Status_OrganizationId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AssetLocation",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Location",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetLocation",
                table: "AssetLocation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLocation_AssetId",
                table: "AssetLocation",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_OrganizationId",
                table: "Location",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLocation_Location_LocationId",
                table: "AssetLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Status_StatusId",
                table: "Assets",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Type_TypeId",
                table: "Assets",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Organizations_OrganizationId",
                table: "Location",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Organizations_OrganizationId",
                table: "Status",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Organizations_OrganizationId",
                table: "Type",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetLocation_Location_LocationId",
                table: "AssetLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Status_StatusId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Type_TypeId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Organizations_OrganizationId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Organizations_OrganizationId",
                table: "Status");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_Organizations_OrganizationId",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetLocation",
                table: "AssetLocation");

            migrationBuilder.DropIndex(
                name: "IX_AssetLocation_AssetId",
                table: "AssetLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_OrganizationId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AssetLocation");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Type_OrganizationId",
                table: "Types",
                newName: "IX_Types_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Status_OrganizationId",
                table: "Statuses",
                newName: "IX_Statuses_OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetLocation",
                table: "AssetLocation",
                columns: new[] { "AssetId", "LocationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetLocation_Locations_LocationId",
                table: "AssetLocation",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Statuses_StatusId",
                table: "Assets",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Types_TypeId",
                table: "Assets",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Organizations_OrganizationId",
                table: "Statuses",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Organizations_OrganizationId",
                table: "Types",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
