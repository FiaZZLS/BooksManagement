using BooksManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAll();
        Task<Book> Get(Guid Id);
        Task<Book> Create(Book entity);
        Task<Book> Update(Guid Id, Book entity);
        Task<bool> Delete(Guid Id);


    }
}
