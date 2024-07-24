using BooksManagement.Models;
using BooksManagement.Services;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFramework.DataRepository
{
    public class BookRepository : IBookService
    {
        private readonly BooksDbContext _dbContext;

        public BookRepository(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> Create(Book entity)
        {
            await _dbContext.Books.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<Book> Get(Guid id)
        {
            return await _dbContext.Books
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        
        public async Task<List<Book>> GetAll()
        {
            return await _dbContext.Books
                .ToListAsync();
        }

        public async Task<Book> Update(Guid id, Book entity)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            book.Title = entity.Title;
            book.AuthorId = entity.AuthorId;
            book.ISBN = entity.ISBN; 
            book.PublicationYear = entity.PublicationYear;

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }
    }
}
