using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_Entities_Vol1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolType",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "PhoneType",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Addresses");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Schools",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Phones",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Addresses",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Addresses");

            migrationBuilder.AddColumn<byte>(
                name: "SchoolType",
                table: "Schools",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "PhoneType",
                table: "Phones",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "AddressType",
                table: "Addresses",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
