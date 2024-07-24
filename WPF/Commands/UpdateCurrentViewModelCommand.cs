using BookManagement.WPF.State.Navigators;
using BookManagement.WPF.ViewModels;
using BooksManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookManagement.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private INavigator _navigator;
        private readonly IBookService _bookservice;
        private readonly IAuthorService _authorservice;
        private readonly ILoanService _loanservice;


        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewtype = (ViewType)parameter;
                switch (viewtype)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.Book:
                        _navigator.CurrentViewModel = new BookViewModel(_bookservice ,_authorservice);
                        break;
                    case ViewType.Author:
                        _navigator.CurrentViewModel = new AuthorViewModel(_authorservice);
                        break;
                    case ViewType.Loan:
                        _navigator.CurrentViewModel = new LoanViewModel(_loanservice ,_bookservice);
                        break;
                    default:
                        break;


                }
            }

        }
    }
}
