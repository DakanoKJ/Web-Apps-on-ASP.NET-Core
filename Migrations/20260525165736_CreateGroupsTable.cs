using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAccount.Migrations
{
    /// <inheritdoc />
    public partial class CreateGroupsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "group_name",
                table: "student_profiles");

            migrationBuilder.AddColumn<int>(
                name: "group_id",
                table: "student_profiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_student_profiles_group_id",
                table: "student_profiles",
                column: "group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_student_profiles_groups_group_id",
                table: "student_profiles",
                column: "group_id",
                principalTable: "groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_profiles_groups_group_id",
                table: "student_profiles");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropIndex(
                name: "IX_student_profiles_group_id",
                table: "student_profiles");

            migrationBuilder.DropColumn(
                name: "group_id",
                table: "student_profiles");

            migrationBuilder.AddColumn<string>(
                name: "group_name",
                table: "student_profiles",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
