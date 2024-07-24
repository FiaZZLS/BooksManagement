using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Borrower {  get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }

    }
}
