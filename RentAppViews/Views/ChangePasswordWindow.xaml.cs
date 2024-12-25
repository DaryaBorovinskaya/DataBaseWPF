using DataBase1WPF.ViewModels;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Timer = System.Windows.Threading.DispatcherTimer;


namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        Timer timerForWindow = new();
        Assembly assembly = Assembly.GetExecutingAssembly();

        

        /// <summary>
        /// Обработчик события загрузки окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerForWindow.Interval = new System.TimeSpan(100);
            timerForWindow.Start();
            timerForWindow.Tick += Timer_Tick;

            textBlockVersion.Text = "Версия" + assembly.GetName().Version.ToString();
        }
        public ChangePasswordWindow()
        {
            InitializeComponent();
            DataContext = new ChangePasswordVM();
            if (DataContext is ChangePasswordVM changePasswordVM)
            {
                changePasswordVM.OnSuccessChangePassword += SuccessChangePassword;
            }
        }


        /// <summary>
        /// Обработчик события Успешная смена пароля
        /// </summary>
        private void SuccessChangePassword()
        {
            this.Close();
        }


        /// <summary>
        /// Обработчик события Таймер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            if (Console.CapsLock)
                textBlockCapsLock.Text = "Клавиша CapsLock нажата";
            else
                textBlockCapsLock.Text = "";

            string languageName = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
            languageName = languageName.Split('(').First();
            string newLanguageName = char.ToUpper(languageName[0]).ToString();

            for (int i = 1; i < languageName.Length; i++)
                newLanguageName += languageName[i];


            textBlockRusEng.Text = "Язык ввода " + newLanguageName;
        }



        /// <summary>
        /// Обработчик события изменения пароля у элемента textBoxOldPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxOldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).OldPassword = ((PasswordBox)sender).Password;
            }
        }


        /// <summary>
        /// Обработчик события изменения пароля у элемента textBoxNewPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password;
            }
        }


        /// <summary>
        /// Обработчик события изменения пароля у элемента textBoxConfirmPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).RepeatedPassword = ((PasswordBox)sender).Password;
            }
        }


        /// <summary>
        /// Обработчик события нажатия на кнопку Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBoxOldPassword.Clear();
            textBoxNewPassword.Clear();
            textBoxConfirmPassword.Clear();
        }
    }
}
