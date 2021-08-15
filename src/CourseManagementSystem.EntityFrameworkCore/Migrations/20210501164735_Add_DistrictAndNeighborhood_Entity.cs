using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Add_DistrictAndNeighborhood_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistrictsAndNeighborhoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DistrictName = table.Column<string>(maxLength: 60, nullable: false),
                    NeighborhoodName = table.Column<string>(maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 6, nullable: true),
                    TownId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictsAndNeighborhoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistrictsAndNeighborhoods_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistrictsAndNeighborhoods_TownId",
                table: "DistrictsAndNeighborhoods",
                column: "TownId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistrictsAndNeighborhoods");
        }
    }
}
