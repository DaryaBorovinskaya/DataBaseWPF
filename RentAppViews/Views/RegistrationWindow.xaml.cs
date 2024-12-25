using DataBase1WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            DataContext = new RegistrationVM();
            if (DataContext is RegistrationVM registrationVM)
            {
                registrationVM.OnSuccessRegistration += SuccessRegistration;
            }
        }

        /// <summary>
        /// Обработчик события успешной регистрации
        /// </summary>
        private void SuccessRegistration()
        {
            this.Close();
        }


        /// <summary>
        /// Обработчик события изменения пароля у элемента textBoxPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        /// <summary>
        /// Обработчик события изменения пароля у элемента textBoxRepeatedPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxRepeatedPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).RepeatedPassword = ((PasswordBox)sender).Password;
            }
        }

    }
}
