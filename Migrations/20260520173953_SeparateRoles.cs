using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAccount.Migrations
{
    /// <inheritdoc />
    public partial class SeparateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_confirmation_tokens_students_student_id",
                table: "confirmation_tokens");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.RenameColumn(
                name: "student_id",
                table: "confirmation_tokens",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_confirmation_tokens_student_id",
                table: "confirmation_tokens",
                newName: "IX_confirmation_tokens_account_id");

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "TEXT", nullable: false),
                    role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "student_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    account_id = table.Column<int>(type: "INTEGER", nullable: false),
                    full_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    group_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    photo_url = table.Column<string>(type: "TEXT", maxLength: 2047, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_profiles_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_student_profiles_account_id",
                table: "student_profiles",
                column: "account_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_confirmation_tokens_accounts_account_id",
                table: "confirmation_tokens",
                column: "account_id",
                principalTable: "accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_confirmation_tokens_accounts_account_id",
                table: "confirmation_tokens");

            migrationBuilder.DropTable(
                name: "student_profiles");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "confirmation_tokens",
                newName: "student_id");

            migrationBuilder.RenameIndex(
                name: "IX_confirmation_tokens_account_id",
                table: "confirmation_tokens",
                newName: "IX_confirmation_tokens_student_id");

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    group_name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "TEXT", nullable: false),
                    photo_url = table.Column<string>(type: "TEXT", maxLength: 2047, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_students_email",
                table: "students",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_confirmation_tokens_students_student_id",
                table: "confirmation_tokens",
                column: "student_id",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
