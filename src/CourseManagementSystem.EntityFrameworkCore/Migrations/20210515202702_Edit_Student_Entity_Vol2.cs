using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_Student_Entity_Vol2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HourlyPayPeriod",
                table: "Students");

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyPaymentAmount",
                table: "Students",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "HourlyPaymentPeriod",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyPaymentAmount",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HourlyPaymentPeriod",
                table: "Students");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "HourlyPayPeriod",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
