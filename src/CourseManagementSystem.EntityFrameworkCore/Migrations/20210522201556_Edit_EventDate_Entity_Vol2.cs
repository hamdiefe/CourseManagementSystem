using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_EventDate_Entity_Vol2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "EventDates",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
