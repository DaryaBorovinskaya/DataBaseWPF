using DataBase1WPF.ViewModels;
using System.Windows;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AboutProgramWindow.xaml
    /// </summary>
    public partial class AboutProgramWindow : Window
    {
        public AboutProgramWindow()
        {
            InitializeComponent();
            DataContext = new AboutProgramVM();
        }
    }
}
