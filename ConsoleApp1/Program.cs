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
        IBookService BookService = new BookRepository(new BooksDbContext());

        for (int i = 0; i <= 10000; i++)
        {
            var authId = new Guid();
            AuthorService.Create(new Author
            {
                Id = authId,
                FirstName = "Test" + i,
                LastName = "Last" + i,
                DateOfBirth = DateTime.Now
            }).Wait();
            BookService.Create(new Book
            {
                Id = new Guid(),
                Title = "Book"+i,
                AuthorId = authId,
                ISBN = "ISBN"+i,
                PublicationYear = DateTime.Now,

            });

        }

}
}