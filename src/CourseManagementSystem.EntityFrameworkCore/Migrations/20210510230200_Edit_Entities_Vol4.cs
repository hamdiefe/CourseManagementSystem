using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_Entities_Vol4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Schools_SchoolId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SchoolId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "GraduationSchoolId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_GraduationSchoolId",
                table: "Teachers",
                column: "GraduationSchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Schools_GraduationSchoolId",
                table: "Teachers",
                column: "GraduationSchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Schools_GraduationSchoolId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_GraduationSchoolId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "GraduationSchoolId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SchoolId",
                table: "Teachers",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Schools_SchoolId",
                table: "Teachers",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
