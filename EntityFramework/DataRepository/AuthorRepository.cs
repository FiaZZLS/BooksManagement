using BooksManagement.Models;
using BooksManagement.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DataRepository
{
    public class AuthorRepository : IAuthorService
    {
            private readonly BooksDbContext _dbContext;

            public AuthorRepository(BooksDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Author> Create(Author entity)
            {
                await _dbContext.Authors.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }

            public async Task<Author> Get(Guid id)
            {
                return await _dbContext.Authors
                    .FirstOrDefaultAsync(a => a.Id == id);
            }

            public async Task<List<Author>> GetAll()
            {
                return await _dbContext.Authors
                    .ToListAsync();
            }

            public async Task<Author> Update(Guid id, Author entity)
            {
                var author = await _dbContext.Authors.FindAsync(id);
                if (author == null) return null;

                author.FirstName = entity.FirstName;
                author.LastName = entity.LastName;
                author.DateOfBirth = entity.DateOfBirth;

                _dbContext.Authors.Update(author);
                await _dbContext.SaveChangesAsync();
                return author;
            }

            public async Task<bool> Delete(Guid id)
            {
                var author = await _dbContext.Authors.FindAsync(id);
                if (author == null) return false;

                _dbContext.Authors.Remove(author);
                await _dbContext.SaveChangesAsync();
                return true;
            }


        
    }
}
