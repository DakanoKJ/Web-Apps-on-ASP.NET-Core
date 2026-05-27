using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAccount.Migrations
{
    /// <inheritdoc />
    public partial class CreateTeacherAndSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_profiles_groups_group_id",
                table: "student_profiles");

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teacher_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    account_id = table.Column<int>(type: "INTEGER", nullable: false),
                    full_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    photo_url = table.Column<string>(type: "TEXT", maxLength: 2047, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_teacher_profiles_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teacher_group_subjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacher_group_subjects", x => x.id);
                    table.ForeignKey(
                        name: "FK_teacher_group_subjects_groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teacher_group_subjects_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teacher_group_subjects_teacher_profiles_TeacherProfileId",
                        column: x => x.TeacherProfileId,
                        principalTable: "teacher_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_teacher_group_subjects_GroupId",
                table: "teacher_group_subjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_group_subjects_SubjectId",
                table: "teacher_group_subjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_group_subjects_TeacherProfileId",
                table: "teacher_group_subjects",
                column: "TeacherProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_profiles_account_id",
                table: "teacher_profiles",
                column: "account_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_student_profiles_groups_group_id",
                table: "student_profiles",
                column: "group_id",
                principalTable: "groups",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_profiles_groups_group_id",
                table: "student_profiles");

            migrationBuilder.DropTable(
                name: "teacher_group_subjects");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "teacher_profiles");

            migrationBuilder.AddForeignKey(
                name: "FK_student_profiles_groups_group_id",
                table: "student_profiles",
                column: "group_id",
                principalTable: "groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
