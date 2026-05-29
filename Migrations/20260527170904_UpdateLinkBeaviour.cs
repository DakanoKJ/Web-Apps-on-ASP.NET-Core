using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAccount.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLinkBeaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teacher_group_subjects_teacher_profiles_TeacherProfileId",
                table: "teacher_group_subjects");

            migrationBuilder.RenameColumn(
                name: "TeacherProfileId",
                table: "teacher_group_subjects",
                newName: "TeacherAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_teacher_group_subjects_TeacherProfileId",
                table: "teacher_group_subjects",
                newName: "IX_teacher_group_subjects_TeacherAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_teacher_group_subjects_accounts_TeacherAccountId",
                table: "teacher_group_subjects",
                column: "TeacherAccountId",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teacher_group_subjects_accounts_TeacherAccountId",
                table: "teacher_group_subjects");

            migrationBuilder.RenameColumn(
                name: "TeacherAccountId",
                table: "teacher_group_subjects",
                newName: "TeacherProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_teacher_group_subjects_TeacherAccountId",
                table: "teacher_group_subjects",
                newName: "IX_teacher_group_subjects_TeacherProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_teacher_group_subjects_teacher_profiles_TeacherProfileId",
                table: "teacher_group_subjects",
                column: "TeacherProfileId",
                principalTable: "teacher_profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
