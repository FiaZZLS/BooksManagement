using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManagement.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title {  get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationYear { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
