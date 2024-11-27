using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editInTranslaion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_newsTranslations_languages_LanguageCode1",
                table: "newsTranslations");

            migrationBuilder.DropIndex(
                name: "IX_newsTranslations_LanguageCode1",
                table: "newsTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_languages",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "newsTranslations");

            migrationBuilder.DropColumn(
                name: "LanguageCode1",
                table: "newsTranslations");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "languages");

            migrationBuilder.RenameTable(
                name: "languages",
                newName: "Languages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "languages");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "newsTranslations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode1",
                table: "newsTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "languages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_languages",
                table: "languages",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_newsTranslations_LanguageCode1",
                table: "newsTranslations",
                column: "LanguageCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_newsTranslations_languages_LanguageCode1",
                table: "newsTranslations",
                column: "LanguageCode1",
                principalTable: "languages",
                principalColumn: "LanguageCode");
        }
    }
}
