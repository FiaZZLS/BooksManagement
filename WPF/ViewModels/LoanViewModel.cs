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
using static System.Reflection.Metadata.BlobBuilder;

namespace BookManagement.WPF.ViewModels
{
    public class LoanViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;

        private Loan _selectedLoan;
        private Book _selectedBook;
        private DateTime _loanDate;
        private DateTime _returnDate;
        private string _borrower;

        public ObservableCollection<Loan> Loans { get; } = new ObservableCollection<Loan>();
        public ObservableCollection<Book> Books { get; } = new ObservableCollection<Book>();

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public LoanViewModel(ILoanService loanService, IBookService bookService)
        {
            _loanService = loanService;
            _bookService = bookService;
            AddCommand = new RelayCommand(_ => AddLoan());
            UpdateCommand = new RelayCommand(_ => UpdateLoan(), _ => SelectedLoan != null);
            DeleteCommand = new RelayCommand(_ => DeleteLoan(), _ => SelectedLoan != null);

            InitializeAsync().ConfigureAwait(false);
        }

        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                _selectedLoan = value;
                OnPropertyChanged();
                if (_selectedLoan != null)
                {
                    LoanDate = _selectedLoan.LoanDate;
                    ReturnDate = _selectedLoan.ReturnDate;
                    Borrower = _selectedLoan.Borrower;
                    SelectedBook = Books.FirstOrDefault(b => b.Id == _selectedLoan.BookId);
                }
            }
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
            }
        }

        public DateTime LoanDate
        {
            get => _loanDate;
            set
            {
                _loanDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnPropertyChanged();
            }
        }

        public string Borrower
        {
            get => _borrower;
            set
            {
                _borrower = value;
                OnPropertyChanged();
            }
        }

        private async void AddLoan()
        {
            var newLoan = new Loan
            {
                Id = Guid.NewGuid(),
                LoanDate = LoanDate,
                ReturnDate = ReturnDate,
                Borrower = Borrower,
                BookId = SelectedBook?.Id ?? Guid.Empty
            };

            await _loanService.Create(newLoan);
            Loans.Add(newLoan);
            ClearInputs();
        }

        private async Task LoadLoans()
        {
            if (_loanService == null)
                throw new InvalidOperationException("LoanService is not initialized.");

            try
            {
                Loans.Clear();
                var loans = await _loanService.GetAll();
                foreach (var loan in loans)
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

        private async Task InitializeAsync()
        {
            await LoadLoans();
            await LoadBooks();
        }

        private async void UpdateLoan()
        {
            if (SelectedLoan == null) return;

            SelectedLoan.LoanDate = LoanDate;
            SelectedLoan.ReturnDate = ReturnDate;
            SelectedLoan.Borrower = Borrower;
            SelectedLoan.BookId = SelectedBook?.Id ?? Guid.Empty;

            await _loanService.Update(SelectedLoan.Id, SelectedLoan);

            var index = Loans.IndexOf(SelectedLoan);
            if (index >= 0)
            {
                Loans[index] = SelectedLoan;
            }
            ClearInputs();
        }

        private async void DeleteLoan()
        {
            if (SelectedLoan == null) return;

            await _loanService.Delete(SelectedLoan.Id);
            Loans.Remove(SelectedLoan);
            ClearInputs();
        }

        private void ClearInputs()
        {
            LoanDate = DateTime.Today;
            ReturnDate = DateTime.Today;
            Borrower = string.Empty;
            SelectedBook = null;
            SelectedLoan = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}