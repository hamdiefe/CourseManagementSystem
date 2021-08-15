using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_Student_Entity_Vol4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnpaidHours",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "PaymentEventlessHours",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentEventlessHours",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "UnpaidHours",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
