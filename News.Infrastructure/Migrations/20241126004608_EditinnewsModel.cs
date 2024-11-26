using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditinnewsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_News_NewsId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_newsTranslations_News_NewsId",
                table: "newsTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_newsTranslations_languages_LanguageCode",
                table: "newsTranslations");

            migrationBuilder.DropIndex(
                name: "IX_newsTranslations_LanguageCode",
                table: "newsTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "images");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "newsTranslations",
                newName: "NewId");

            migrationBuilder.RenameIndex(
                name: "IX_newsTranslations_NewsId",
                table: "newsTranslations",
                newName: "IX_newsTranslations_NewId");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "News",
                newName: "NewId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_NewsId",
                table: "images",
                newName: "IX_images_NewsId");

            migrationBuilder.AlterColumn<string>(
                name: "LanguageCode",
                table: "newsTranslations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "newsTranslations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode1",
                table: "newsTranslations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "language",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_images",
                table: "images",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_newsTranslations_LanguageCode1",
                table: "newsTranslations",
                column: "LanguageCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_images_News_NewsId",
                table: "images",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "NewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_newsTranslations_News_NewId",
                table: "newsTranslations",
                column: "NewId",
                principalTable: "News",
                principalColumn: "NewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_newsTranslations_languages_LanguageCode1",
                table: "newsTranslations",
                column: "LanguageCode1",
                principalTable: "languages",
                principalColumn: "LanguageCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_News_NewsId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_newsTranslations_News_NewId",
                table: "newsTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_newsTranslations_languages_LanguageCode1",
                table: "newsTranslations");

            migrationBuilder.DropIndex(
                name: "IX_newsTranslations_LanguageCode1",
                table: "newsTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_images",
                table: "images");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "newsTranslations");

            migrationBuilder.DropColumn(
                name: "LanguageCode1",
                table: "newsTranslations");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "News");

            migrationBuilder.DropColumn(
                name: "language",
                table: "News");

            migrationBuilder.RenameTable(
                name: "images",
                newName: "Images");

            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "newsTranslations",
                newName: "NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_newsTranslations_NewId",
                table: "newsTranslations",
                newName: "IX_newsTranslations_NewsId");

            migrationBuilder.RenameColumn(
                name: "NewId",
                table: "News",
                newName: "NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_images_NewsId",
                table: "Images",
                newName: "IX_Images_NewsId");

            migrationBuilder.AlterColumn<string>(
                name: "LanguageCode",
                table: "newsTranslations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_newsTranslations_LanguageCode",
                table: "newsTranslations",
                column: "LanguageCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_News_NewsId",
                table: "Images",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "NewsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_newsTranslations_News_NewsId",
                table: "newsTranslations",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "NewsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_newsTranslations_languages_LanguageCode",
                table: "newsTranslations",
                column: "LanguageCode",
                principalTable: "languages",
                principalColumn: "LanguageCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
