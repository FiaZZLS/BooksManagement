using BooksManagement.Models;
using Microsoft.EntityFrameworkCore;


namespace EntityFramework
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Loan>().HasKey(l => l.Id);

            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Author)
                        .WithMany(a => a.Books)
                        .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-Q10M5ILK;Database=BooksManagement;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }
    }
}
