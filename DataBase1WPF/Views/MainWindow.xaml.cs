using DataBase1WPF.ViewModels;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using DataBase1WPF.Views.Constructors;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MenuConstructor menuConstructor = new();
            constructedMenu.Children.Add(menuConstructor.Construct());

            if (DataContext is MainVM mainVM)
            {
                mainVM.OnRegistration += Registration;
                mainVM.OnChangePassword += ChangePassword;
                mainVM.OnSQLquery += SQLquery;
            }
        }

        private void Registration()
        {
            RegistrationWindow window = new();
            window.Show();
            
        }

        private void ChangePassword()
        {
            ChangePasswordWindow window = new();
            window.Show();
        }

        private void SQLquery()
        {
            SQLqueryWindow window = new ();
            window.Show();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
