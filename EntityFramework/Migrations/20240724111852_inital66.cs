using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class inital66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "returnDate",
                table: "Loans",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "loanDate",
                table: "Loans",
                newName: "LoanDate");

            migrationBuilder.RenameColumn(
                name: "borrower",
                table: "Loans",
                newName: "Borrower");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "publicationYear",
                table: "Books",
                newName: "PublicationYear");

            migrationBuilder.RenameColumn(
                name: "authorId",
                table: "Books",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Authors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Authors",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "dateOfBirth",
                table: "Authors",
                newName: "DateOfBirth");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Loans",
                newName: "returnDate");

            migrationBuilder.RenameColumn(
                name: "LoanDate",
                table: "Loans",
                newName: "loanDate");

            migrationBuilder.RenameColumn(
                name: "Borrower",
                table: "Loans",
                newName: "borrower");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "PublicationYear",
                table: "Books",
                newName: "publicationYear");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Books",
                newName: "authorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                newName: "IX_Books_authorId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Authors",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Authors",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Authors",
                newName: "dateOfBirth");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authorId",
                table: "Books",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
