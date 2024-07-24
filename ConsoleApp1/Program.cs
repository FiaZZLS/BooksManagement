using BooksManagement.Models;
using BooksManagement.Services;
using EntityFramework.DataRepository;
using EntityFramework;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IAuthorService AuthorService = new AuthorRepository(new BooksDbContext());
            IBookService BookService = new BookRepository(new BooksDbContext());

            for (int i = 0; i <= 10000; i++)
            {
                var authId = Guid.NewGuid();
                var author = new Author
                {
                    Id = authId,
                    FirstName = "Test" + i,
                    LastName = "Last" + i,
                    DateOfBirth = DateTime.Now
                };
                await AuthorService.Create(author);

                var book = new Book
                {
                    Id = Guid.NewGuid(), 
                    Title = "Book" + i,
                    AuthorId = authId,
                    ISBN = "ISBN" + i,
                    PublicationYear = DateTime.Now
                };
                await BookService.Create(book);
            }
        }
    }
}
