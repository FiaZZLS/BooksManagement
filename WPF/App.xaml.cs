using BookManagement.WPF.View;
using BookManagement.WPF.ViewModels;
using BooksManagement.Models;
using EntityFramework.DataRepository;
using EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.DataContext = new MainViewModel();
            window.Show();
            base.OnStartup(e);
        }

    }

}
