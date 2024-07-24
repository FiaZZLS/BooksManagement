using BookManagement.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookManagement.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Book,
        Author,
        Loan
    }
    public interface INavigator
    {
        public ViewModelBase CurrentViewModel { get; set; }
        ICommand updateCurrentViewModelCommand { get; } 
    }
}
