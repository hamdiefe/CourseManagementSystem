using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Migrations
{
    public partial class Add_Parent_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Jobs_JobId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "EducationalStatus",
                table: "Parents",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Parents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ParentId",
                table: "Phones",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_StudentId",
                table: "Parents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ParentId",
                table: "Addresses",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Parents_ParentId",
                table: "Addresses",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_Students_StudentId",
                table: "Parents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Parents_ParentId",
                table: "Phones",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Parents_ParentId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_Students_StudentId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Parents_ParentId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_ParentId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Parents_StudentId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ParentId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "EducationalStatus",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobId",
                table: "Jobs",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Jobs_JobId",
                table: "Jobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
