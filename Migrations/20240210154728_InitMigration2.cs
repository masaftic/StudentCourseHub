using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourseHub.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Students_studentsStudentId",
                table: "CourseStudent");

            migrationBuilder.RenameColumn(
                name: "studentsStudentId",
                table: "CourseStudent",
                newName: "StudentsStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_studentsStudentId",
                table: "CourseStudent",
                newName: "IX_CourseStudent_StudentsStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Students_StudentsStudentId",
                table: "CourseStudent",
                column: "StudentsStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Students_StudentsStudentId",
                table: "CourseStudent");

            migrationBuilder.RenameColumn(
                name: "StudentsStudentId",
                table: "CourseStudent",
                newName: "studentsStudentId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_StudentsStudentId",
                table: "CourseStudent",
                newName: "IX_CourseStudent_studentsStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Students_studentsStudentId",
                table: "CourseStudent",
                column: "studentsStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
