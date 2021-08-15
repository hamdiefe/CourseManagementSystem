using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_EventDate_Entity_Vol4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDates_Students_StudentId",
                table: "EventDates");

            migrationBuilder.DropIndex(
                name: "IX_EventDates_StudentId",
                table: "EventDates");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "EventDates");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "EventDates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "EventDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "EventDates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventDates_StudentId",
                table: "EventDates",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventDates_Students_StudentId",
                table: "EventDates",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
