using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_StudentParent_Entity_Vol5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "StudentParents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "StudentParents",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "StudentParents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "StudentParents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "StudentParents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "StudentParents",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "StudentParents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
