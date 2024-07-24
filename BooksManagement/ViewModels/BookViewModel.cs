using BooksManagement.Commands;
using BooksManagement.Models;
using BooksManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BooksManagement.ViewModels
{
    public class BookViewModel
    {
        private readonly IBookService _bookRepository;
        public ObservableCollection<Book> Books { get; set; }

        public ICommand AddBookCommand { get; }
        public ICommand UpdateBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand LoadBooksCommand { get; }

        public BookViewModel(IBookService bookRepository)
        {
            _bookRepository = bookRepository;
            Books = new ObservableCollection<Book>();
            AddBookCommand = new RelayCommand(async (param) => await AddBook(), CanAddBook);
            UpdateBookCommand = new RelayCommand(async (param) => await UpdateBook(), CanUpdateBook);
            DeleteBookCommand = new RelayCommand(async (param) => await DeleteBook(), CanDeleteBook);
            LoadBooksCommand = new RelayCommand(async (param) => await LoadBooks());
        }

        private async Task AddBook()
        {
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "New Book",
                AuthorId = Guid.NewGuid(), 
                ISBN = "1234567890",
                PublicationYear = DateTime.Now
            };

            var book = await _bookRepository.Create(newBook);
            Books.Add(book);
        }

        private async Task UpdateBook()
        {
        }

        private async Task DeleteBook()
        {
        }

        private async Task LoadBooks()
        {
            var books = await _bookRepository.GetAll();
            Books.Clear();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        private bool CanAddBook(object parameter) => true;
        private bool CanUpdateBook(object parameter) => true;
        private bool CanDeleteBook(object parameter) => true;
    }
}