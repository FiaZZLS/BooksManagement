using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class inital4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authorID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "authorID",
                table: "Books",
                newName: "authorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authorID",
                table: "Books",
                newName: "IX_Books_authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "Books",
                newName: "authorID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authorId",
                table: "Books",
                newName: "IX_Books_authorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authorID",
                table: "Books",
                column: "authorID",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
