using DataBase1WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            if (DataContext is RegistrationVM registrationVM)
            {
                registrationVM.OnSuccessRegistration += SuccessRegistration;
            }
        }

        private void SuccessRegistration()
        {
            this.Close();
        }

        private void textBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void textBoxRepeatedPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).RepeatedPassword = ((PasswordBox)sender).Password;
            }
        }

    }
}
