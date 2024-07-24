using BooksManagement.Models;
using BooksManagement.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Commands;

namespace BookManagement.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase,INotifyPropertyChanged
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;

        private string _borrowerFilter;
        private Book _selectedBookFilter;

        public ObservableCollection<Loan> Loans { get; } = new ObservableCollection<Loan>();
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        public ICommand FilterLoansCommand { get; }

        public HomeViewModel(ILoanService loanService, IBookService bookService)
        {
            _loanService = loanService;
            _bookService = bookService;
            FilterLoansCommand = new RelayCommand(_ => LoadFilteredLoans());

            InitializeAsync().ConfigureAwait(false);
        }

        public string BorrowerFilter
        {
            get => _borrowerFilter;
            set
            {
                _borrowerFilter = value;
                OnPropertyChanged();
            }
        }

        public Book SelectedBookFilter
        {
            get => _selectedBookFilter;
            set
            {
                _selectedBookFilter = value;
                OnPropertyChanged();
            }
        }

        private async Task InitializeAsync()
        {
            await LoadBooks();
            await LoadFilteredLoans();
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

        private async Task LoadFilteredLoans()
        {
            if (_loanService == null)
                throw new InvalidOperationException("LoanService is not initialized.");

            try
            {
                Loans.Clear();
                var loans = await _loanService.GetAll();
                var filteredLoans = loans.AsQueryable();

                if (!string.IsNullOrWhiteSpace(BorrowerFilter))
                {
                    filteredLoans = filteredLoans.Where(l => l.Borrower.Contains(BorrowerFilter, StringComparison.OrdinalIgnoreCase));
                }

                if (SelectedBookFilter != null)
                {
                    filteredLoans = filteredLoans.Where(l => l.BookId == SelectedBookFilter.Id);
                }

                foreach (var loan in filteredLoans)
                {
                    Loans.Add(loan);
                }
                Console.WriteLine($"Loaded {Loans.Count} loans.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching loans: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
