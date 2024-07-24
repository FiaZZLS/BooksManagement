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
    public class AuthorViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly IAuthorService _authorService;

        private Author _selectedAuthor;
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;

        public ObservableCollection<Author> Authors { get; } = new ObservableCollection<Author>();

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public AuthorViewModel(IAuthorService authorService)
        {
            _authorService = authorService;
            AddCommand = new RelayCommand(_ => AddAuthor());
            UpdateCommand = new RelayCommand(_ => UpdateAuthor(), _ => SelectedAuthor != null);
            DeleteCommand = new RelayCommand(_ => DeleteAuthor(), _ => SelectedAuthor != null);
            InitializeAsync().ConfigureAwait(false);

        }

        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged();
                if (_selectedAuthor != null)
                {
                    FirstName = _selectedAuthor.FirstName;
                    LastName = _selectedAuthor.LastName;
                    DateOfBirth = _selectedAuthor.DateOfBirth;
                }
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        private async void AddAuthor()
        {
            var newAuthor = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth
            };

            await _authorService.Create(newAuthor);
            Authors.Add(newAuthor);
            ClearInputs();
        }

        private async Task LoadAuthors()
        {
            if (_authorService == null)
                throw new InvalidOperationException("BookService is not initialized.");

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
            await LoadAuthors();
        }

        private async void UpdateAuthor()
        {
            if (SelectedAuthor == null) return;

            SelectedAuthor.FirstName = FirstName;
            SelectedAuthor.LastName = LastName;
            SelectedAuthor.DateOfBirth = DateOfBirth;

            await _authorService.Update(SelectedAuthor.Id, SelectedAuthor);
            var index = Authors.IndexOf(SelectedAuthor);
            if (index >= 0)
            {
                Authors[index] = SelectedAuthor;
            }
            ClearInputs();
        }

        private async void DeleteAuthor()
        {
            if (SelectedAuthor == null) return;

            await _authorService.Delete(SelectedAuthor.Id);
            Authors.Remove(SelectedAuthor);
            ClearInputs();
        }

        private void ClearInputs()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Today;
            SelectedAuthor = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
