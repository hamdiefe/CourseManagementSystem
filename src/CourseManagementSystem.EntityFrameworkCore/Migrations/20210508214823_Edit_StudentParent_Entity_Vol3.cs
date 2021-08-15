using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_StudentParent_Entity_Vol3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "StudentParents");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "StudentParents");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "StudentParents");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "StudentParents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "StudentParents");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "StudentParents");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "StudentParents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "StudentParents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "StudentParents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "StudentParents",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "StudentParents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentParents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "StudentParents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "StudentParents",
                type: "bigint",
                nullable: true);
        }
    }
}
