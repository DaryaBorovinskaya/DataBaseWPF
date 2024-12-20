using DataBase1WPF.ViewModels;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Timer = System.Windows.Threading.DispatcherTimer;

namespace DataBase1WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        Timer timerForWindow = new ();
        Assembly assembly = Assembly.GetExecutingAssembly();

        public LogInWindow()
        {
            InitializeComponent();

            if (DataContext is LogInVM loginVM)
            {
                loginVM.OnLogInSuccess += LogInSuccess;
            }
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerForWindow.Interval = new System.TimeSpan(100);
            timerForWindow.Start();
            timerForWindow.Tick += Timer_Tick;

            textBlockVersion.Text = "Версия" + assembly.GetName().Version.ToString();
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


        private void textBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; 
            }
        }

        
        private void LogInSuccess()
        {
            MainWindow window = new();
            window.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBoxPassword.Clear();
        }
    }
}
