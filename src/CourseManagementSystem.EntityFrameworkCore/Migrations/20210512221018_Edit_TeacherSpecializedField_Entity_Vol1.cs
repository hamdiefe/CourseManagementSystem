using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Edit_TeacherSpecializedField_Entity_Vol1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecializedFields_Teachers_TeacherId",
                table: "SpecializedFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecializedFields",
                table: "SpecializedFields");

            migrationBuilder.RenameTable(
                name: "SpecializedFields",
                newName: "TeacherSpecializedFields");

            migrationBuilder.RenameIndex(
                name: "IX_SpecializedFields_TeacherId",
                table: "TeacherSpecializedFields",
                newName: "IX_TeacherSpecializedFields_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSpecializedFields",
                table: "TeacherSpecializedFields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSpecializedFields_Teachers_TeacherId",
                table: "TeacherSpecializedFields",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSpecializedFields_Teachers_TeacherId",
                table: "TeacherSpecializedFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSpecializedFields",
                table: "TeacherSpecializedFields");

            migrationBuilder.RenameTable(
                name: "TeacherSpecializedFields",
                newName: "SpecializedFields");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSpecializedFields_TeacherId",
                table: "SpecializedFields",
                newName: "IX_SpecializedFields_TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecializedFields",
                table: "SpecializedFields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecializedFields_Teachers_TeacherId",
                table: "SpecializedFields",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
