using BooksManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Services
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();
        Task<Author> Get(Guid Id);
        Task<Author> Create(Author entity);
        Task<Author> Update(Guid Id, Author entity);
        Task<bool> Delete(Guid Id);


    }
}
