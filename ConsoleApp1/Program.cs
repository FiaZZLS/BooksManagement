using BooksManagement.Models;
using BooksManagement.Services;
using EntityFramework;
using EntityFramework.DataRepository;
namespace ConsoleApp1;
class Program
{
    static void Main(string[] args)
    {
        IAuthorService AuthorService = new AuthorRepository(new BooksDbContext());
        AuthorService.Create(new Author
        {
            Id = new Guid(),
            FirstName = "test2",
            LastName = "test2",
            DateOfBirth = DateTime.Now

        }).Wait();
        AuthorService.GetAll().Wait();
        IBookService BookService = new BookRepository(new BooksDbContext());
        BookService.Create(new Book
        {
            Id = new Guid(),
            Title = "Book1",
            AuthorId = new Guid("49C5253C-1350-42E6-159C-08DCABD2B2C7"),
            ISBN = "ISBN1",
            PublicationYear = DateTime.Now,

        });
        ILoanService LoanService = new LoanRepository(new BooksDbContext());
        LoanService.Create(new Loan
        {
            Id = new Guid(),
            LoanDate = DateTime.Now,
            ReturnDate = DateTime.Now.AddDays(1),
            Borrower = "Name1",
            BookId= new Guid("F5C6CA96-99C5-44FA-1EBB-08DCABD46A38")

        });

}
}