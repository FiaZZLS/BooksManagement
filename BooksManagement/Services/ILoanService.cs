using BooksManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Services
{
    public interface ILoanService
    {
        Task<List<Loan>> GetAll();
        Task<Loan> Get(Guid Id);
        Task<Loan> Create(Loan entity);
        Task<Loan> Update(Guid Id, Loan entity);
        Task<bool> Delete(Guid Id);
        Task<List<Loan>> GetLoansByBorrower(string BorrowerId);
        Task<List<Loan>> getLoansByBook(Guid BookId);
    }
}
