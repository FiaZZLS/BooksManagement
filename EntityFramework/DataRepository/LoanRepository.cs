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
    public class LoanRepository : ILoanService
    {
        private readonly BooksDbContext _dbContext;

        public LoanRepository(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loan> Create(Loan entity)
        {
            await _dbContext.Loans.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Loan> Get(Guid id)
        {
            return await _dbContext.Loans
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Loan>> GetAll()
        {
            return await _dbContext.Loans
                .ToListAsync();
        }

        public async Task<Loan> Update(Guid id, Loan entity)
        {
            var loan = await _dbContext.Loans.FindAsync(id);
            if (loan == null) return null;

            loan.LoanDate = entity.LoanDate;
            loan.ReturnDate = entity.ReturnDate;
            loan.Borrower = entity.Borrower;
            loan.BookId = entity.BookId;

            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }

        public async Task<bool> Delete(Guid id)
        {
            var loan = await _dbContext.Loans.FindAsync(id);
            if (loan == null) return false;

            _dbContext.Loans.Remove(loan);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Loan>> GetLoansByBorrower(string BorrowerName)
        {
            return await _dbContext.Loans
                .Where(f => (f.Borrower == BorrowerName)).ToListAsync();
        }

        public async Task<List<Loan>> getLoansByBook(Guid BookId)
        {
            return await _dbContext.Loans
                .Where(f => (f.BookId == BookId)).ToListAsync();
        }
    }
}
