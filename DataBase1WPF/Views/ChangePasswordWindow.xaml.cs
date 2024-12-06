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
            if (DataContext is ChangePasswordVM changePasswordVM)
            {
                changePasswordVM.OnSuccessChangePassword += SuccessChangePassword;
            }
        }

        private void SuccessChangePassword()
        {
            this.Close();
        }

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

        private void textBoxOldPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).OldPassword = ((PasswordBox)sender).Password;
            }
        }

        private void textBoxNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password;
            }
        }

        private void textBoxConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).RepeatedPassword = ((PasswordBox)sender).Password;
            }
        }

        
    }
}
