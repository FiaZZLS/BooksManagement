using BooksManagement.Models;
using BooksManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;

namespace BookManagement.WPF.ViewModels
{
    public class BookViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        private Book _selectedBook;
        private Author _selectedAuthor;
        private string _title;
        private string _isbn;
        private DateTime _publicationYear;

        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();
        public ObservableCollection<Author> Authors { get; } = new ObservableCollection<Author>();

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public BookViewModel(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;

            AddCommand = new RelayCommand(_ => AddBook());
            UpdateCommand = new RelayCommand(_ => UpdateBook(), _ => SelectedBook != null);
            DeleteCommand = new RelayCommand(_ => DeleteBook(), _ => SelectedBook != null);

            InitializeAsync().ConfigureAwait(false);
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                if (_selectedBook != null)
                {
                    Title = _selectedBook.Title;
                    ISBN = _selectedBook.ISBN;
                    PublicationYear = _selectedBook.PublicationYear;
                    SelectedAuthor = Authors.FirstOrDefault(a => a.Id == _selectedBook.AuthorId);
                }
            }
        }

        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                _isbn = value;
                OnPropertyChanged();
            }
        }

        public DateTime PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChanged();
            }
        }

        private async void AddBook()
        {
            var newBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = Title,
                ISBN = ISBN,
                PublicationYear = PublicationYear,
                AuthorId = SelectedAuthor?.Id ?? Guid.Empty 
            };

            await _bookService.Create(newBook);
            Books.Add(newBook);
            ClearInputs();
        }

        private async Task LoadBooks()
        {
            if (_bookService == null)
                throw new InvalidOperationException("BookService is not initialized.");

            try
            {
                Books.Clear();
                var books = await _bookService.GetAll();
                foreach (var book in books)
                {
                    Books.Add(book);
                }
                Console.WriteLine($"Loaded {Books.Count} books.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching books: {ex.Message}");
            }
        }

        private async Task LoadAuthors()
        {
            if (_authorService == null)
                throw new InvalidOperationException("AuthorService is not initialized.");

            try
            {
                Authors.Clear();
                var authors = await _authorService.GetAll();
                foreach (var author in authors)
                {
                    Authors.Add(author);
                }
                Console.WriteLine($"Loaded {Authors.Count} authors.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching authors: {ex.Message}");
            }
        }

        private async Task InitializeAsync()
        {
            await LoadBooks();
            await LoadAuthors();
        }

        private async void UpdateBook()
        {
            if (SelectedBook == null) return;

            SelectedBook.Title = Title;
            SelectedBook.ISBN = ISBN;
            SelectedBook.PublicationYear = PublicationYear;
            SelectedBook.AuthorId = SelectedAuthor?.Id ?? Guid.Empty;

            await _bookService.Update(SelectedBook.Id, SelectedBook);

            var index = Books.IndexOf(SelectedBook);
            if (index >= 0)
            {
                Books[index] = SelectedBook;
            }
            ClearInputs();
        }

        private async void DeleteBook()
        {
            if (SelectedBook == null) return;

            await _bookService.Delete(SelectedBook.Id);
            Books.Remove(SelectedBook);
            ClearInputs();
        }

        private void ClearInputs()
        {
            Title = string.Empty;
            ISBN = string.Empty;
            PublicationYear = DateTime.Today;
            SelectedAuthor = null;
            SelectedBook = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}